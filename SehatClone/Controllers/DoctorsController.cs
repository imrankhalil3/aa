using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SehatClone.Models;
using SehatClone.ViewModel;

namespace SehatClone.Controllers
{
    public class DoctorsController : Controller
    {
        readonly ApplicationDbContext db;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public DoctorsController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Detail(int id)
        {
            var doctor = db.Doctors.Include(d => d.Speciality)
                            .SingleOrDefault(d => d.Id == id && d.IsApproved);

            if (doctor == null)
                return HttpNotFound("Doctor Not Found");

            return View(doctor);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(CreateDoctorVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var doctor = new Doctor();
            doctor.Name = vm.Name;
            doctor.Email = vm.Email;
            doctor.Password = vm.Password;
            doctor.ConfirmPassword = vm.ConfirmPassword;
            doctor.Phone = vm.Phone;
            doctor.Gender = vm.Gender;
            doctor.City = vm.City;
            doctor.IsApproved = false;

            var specialityInDb = db.Specialities.SingleOrDefault(s => s.Name.ToLower().Equals(vm.Speciality));

            if (specialityInDb == null)
            {
                var speciality = new Speciality();
                speciality.Name = vm.Speciality;

                db.Specialities.Add(speciality);
                doctor.Speciality.Add(speciality);
                db.SaveChanges();
            }
            else
                doctor.Speciality.Add(specialityInDb);

            db.Doctors.Add(doctor);
            db.SaveChanges();

            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email };
            var result = await UserManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return View(vm);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var doctor = db.Doctors.SingleOrDefault(d => d.Email == model.Email && d.Password == model.Password);

            if (doctor == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }


            if (!doctor.IsApproved)
            {
                ModelState.AddModelError("", "Not Approved By Admin Yet");
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
    }
}