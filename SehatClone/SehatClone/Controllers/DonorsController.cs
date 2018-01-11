using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SehatClone.Models;
using SehatClone.ViewModel;

namespace SehatClone.Controllers
{
    public class DonorsController : Controller
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

        public DonorsController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Register()
        {
            var viewModel = new CreateDonorVm
            {
                BloodTypes = new List<string>() { "O-", "O+", "A-", "A+", "B+", "B-", "AB+", "AB-", "A", "B", "AB", "O" }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Register(CreateDonorVm vm)
        {
            vm.BloodTypes = new List<string>() { "O-", "O+", "A-", "A+", "B+", "B-", "AB+", "AB-", "A", "B", "AB", "O" };

            if (!ModelState.IsValid)
                return View(vm);

            var donor = new Donor();
            donor.Name = vm.Name;
            donor.Email = vm.Email;
            donor.Type = vm.Type;
            donor.Password = vm.Password;
            donor.ConfirmPassword = vm.ConfirmPassword;
            donor.Phone = vm.Phone;
            donor.Location = vm.Location;
            donor.IsApproved = false;

            db.Donors.Add(donor);
            db.SaveChanges();

            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email };
            var result = await UserManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
                return RedirectToAction("Login");

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

            var donor = db.Donors.SingleOrDefault(d => d.Email == model.Email && d.Password == model.Password);

            if (donor == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }


            if (!donor.IsApproved)
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