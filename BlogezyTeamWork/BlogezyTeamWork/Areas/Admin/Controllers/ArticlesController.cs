//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BlogezyTeamWork.Data;
//using BlogezyTeamWork.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BlogezyTeamWork.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Authorize]
//    public class ArticlesController : Controller
//    {

//        private BlogezyDbContext _db;
//        public ArticlesController(BlogezyDbContext db)
//        {
//            _db = db;
//        }


//        [HttpGet]
//        public IActionResult List()
//        {
//            //Declare a list of ProductVM
//            List<Article> Articles;

//            using (_db)
//            {
//                //Init the list
//                Articles = _db.Articles.Include(m => m.ArticleCategory).ToList();
//            }

//            //Return view with list
//            return View(Articles);
//        }

//        [HttpGet]
//        public IActionResult Add()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Add(ArticleModel articleModel)
//        {
//            if (ModelState.IsValid)
//            {
//                articleModel.Author = HttpContext.Session.GetString("id");
//                string filename = "";

//                if (articleModel.File.ContentType == "image/png")
//                {
//                    filename = Guid.NewGuid() + articleModel.File.FileName;
//                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
//                    using (FileStream fs = new FileStream(path, FileMode.Create))
//                    {
//                        articleModel.File.CopyTo(fs);
//                    }

//                }


//                Article article = articleModel;
//                article.PhotoPath = filename;


//                await _articleBlogDbContext.Articles.AddAsync(article);
//                await _articleBlogDbContext.SaveChangesAsync();
//                return RedirectToAction(nameof(List));
//            }
//            else
//                return View();
//        }


//        [HttpGet]
//        public ActionResult DeleteProduct(int id)
//        {
//            string path;
//            //Delete article from DB
//            using (_articleBlogDbContext)
//            {
//                Article article = _articleBlogDbContext.Articles.Find(id);
//                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", article.PhotoPath);
//                _articleBlogDbContext.Articles.Remove(article);

//                _articleBlogDbContext.SaveChanges();
//            }
//            System.IO.File.Delete(path);

//            //Redirect
//            return RedirectToAction(nameof(List));
//        }




//        // GET: Admin/article/id
//        [HttpGet]
//        public ActionResult Edit(int id)
//        {
//            //Declare article
//            Article article;

//            using (_articleBlogDbContext)
//            {
//                //Get the article
//                article = _articleBlogDbContext.Articles.Find(id);

//                //Make sure article exists
//                if (article == null)
//                {
//                    return Content("That product does not exist.");
//                }
//            }

//            //Return view with model
//            return View(article);
//        }

//        // POST: Admin/article/id
//        [HttpPost]
//        public ActionResult Edit(Article article)
//        {
//            //Get article id
//            int id = article.Id;

//            //Check model state
//            if (!ModelState.IsValid)
//            {
//                return View(article);
//            }

//            //Make sure article name is unique


//            if (_articleBlogDbContext.Articles.Where(x => x.Id != id).Any(x => x.Name == article.Name))
//            {
//                ModelState.AddModelError("", "That product name is taken!");
//                return View(article);
//            }




//            //Update article


//            Article art = _articleBlogDbContext.Articles.Find(id);
//            string path;
//            string filename;

//            if (article.File.FileName != null)
//            {
//                if (article.File.ContentType == "image/png")
//                {
//                    filename = Guid.NewGuid() + article.File.FileName;
//                    string pathh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
//                    using (FileStream fs = new FileStream(pathh, FileMode.Create))
//                    {
//                        article.File.CopyTo(fs);
//                    }
//                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", art.PhotoPath);
//                    System.IO.File.Delete(path);
//                    art.PhotoPath = filename;

//                }
//            }

//            art.Name = article.Name;
//            art.Description = article.Description;
//            art.MetaDescription = article.MetaDescription;
//            art.MetaKeywords = article.MetaKeywords;
//            art.Tags = article.Tags;
//            _articleBlogDbContext.SaveChanges();


//            //Set TempData message
//            TempData["SM"] = "You have edited the product!";


//            //Redirect
//            return RedirectToAction(nameof(List));
//        }

//    }