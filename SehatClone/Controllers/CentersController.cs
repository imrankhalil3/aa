using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SehatClone.Models;

namespace SehatClone.Controllers
{
    public class CentersController : Controller
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

        public CentersController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Detail(int id)
        {
            var center = db.Centers.Include(c => c.Services)
                            .SingleOrDefault(c => c.Id == id);

            if (center == null)
                return HttpNotFound("Center Not Found");

            return View(center);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Center center)
        {
            if (!ModelState.IsValid)
                return View(center);

            db.Centers.Add(center);
            db.SaveChanges();

            var user = new ApplicationUser { UserName = center.Email, Email = center.Email };
            var result = await UserManager.CreateAsync(user, center.Password);
            if (result.Succeeded)
            {
                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return View(center);
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

            var center = db.Centers.SingleOrDefault(d => d.Email == model.Email && d.Password == model.Password);

            if (center == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            if (!center.IsApproved)
            {
                ModelState.AddModelError("", "Not Approved By Admin Yet");
                return View(model);
            }

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