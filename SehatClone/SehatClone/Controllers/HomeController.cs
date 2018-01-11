using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SehatClone.Models;
using SehatClone.ViewModel;

namespace SehatClone.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchVm searchVm)
        {
            if (searchVm.UserType == "doctor")
            {
                var doctors = SearchDoctors(searchVm);
                return View("Doctors", doctors);
            }
            else if (searchVm.UserType == "center")
            {
                var centers = SearchCenters(searchVm);
                return View("Centers", centers);
            }

            var donors = SearchDonors(searchVm);
            return View("Donors", donors);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        List<Doctor> SearchDoctors(SearchVm searchVm)
        {
            var doctors = db.Doctors.Include(d => d.Speciality)
                .Where(d => (d.Name.ToLower().Contains(searchVm.SearchTxt.ToLower()) ||
                            d.Speciality.Select(c => c.Name.ToLower()).Contains(searchVm.SearchTxt.ToLower()) ||
                            searchVm.Location.ToLower().Contains(d.City.ToLower())) && d.IsApproved);

            return doctors.ToList();
        }

        List<Center> SearchCenters(SearchVm searchVm)
        {
            var centers = db.Centers.Include(c => c.Type)
                .Where(c => (c.Name.ToLower().Contains(searchVm.SearchTxt.ToLower()) ||
                            c.Type.Name.ToLower().Contains(searchVm.SearchTxt.ToLower()) ||
                            searchVm.Location.ToLower().Contains(c.City.ToLower())) && c.IsApproved);

            return centers.ToList();
        }

        List<Donor> SearchDonors(SearchVm searchVm)
        {
            var donors = db.Donors
                            .Where(c => (c.Name.ToLower().Contains(searchVm.SearchTxt.ToLower()) ||
                                         c.Type.ToLower().Contains(searchVm.SearchTxt.ToLower()) ||
                                         c.Location.ToLower().Contains(searchVm.Location.ToLower()))
                                         && c.IsApproved);
            return donors.ToList();
        }
    }
}