using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SehatClone.Models;
using SehatClone.ViewModel;

namespace SehatClone.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        readonly ApplicationDbContext db;

        public ArticlesController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Type)
                                .OrderByDescending(a => a.Id).ToList();
            return View(articles);
        }

        [AllowAnonymous]
        public ActionResult Show(string article)
        {
            var articles = new List<Article>();
            if (!string.IsNullOrEmpty(article))
                articles = db.Articles.Include(a => a.Type).Where(a => a.Title.ToLower().Contains(article.ToLower())).ToList();
            else
                articles = db.Articles.Include(a => a.Type).ToList();

            return View(articles.OrderByDescending(a => a.Id));
        }

        public ActionResult Create()
        {
            var vm = new CreateArticleVm
            {
                Categories = db.ArticleCategories.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CreateArticleVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var cat = db.ArticleCategories.SingleOrDefault(a => a.Id == vm.Type);

            var article = new Article
            {
                Title = vm.Title,
                Content = vm.Content,
                DateTime = DateTime.Now,
                Type = cat
            };

            db.Articles.Add(article);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var article = db.Articles.Find(id);

            if (article == null)
                return HttpNotFound("Article not Found !");

            db.Articles.Remove(article);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}