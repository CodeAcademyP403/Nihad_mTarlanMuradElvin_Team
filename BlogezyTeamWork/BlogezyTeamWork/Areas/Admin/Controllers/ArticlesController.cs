using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogezyTeamWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticlesController : Controller
    {

        private BlogezyDbContext _db;
        public ArticlesController(BlogezyDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult List()
        {
            //Declare a list of ProductVM
            List<Article> Articles;

            using (_db)
            {
                //Init the list
                Articles = _db.Articles.Include(m => m.ArticleCategory).ToList();
            }

            //Return view with list
            return View(Articles);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Article article)
        {
            if (ModelState.IsValid)
            {
                string filename = "";

                if (article.File.ContentType == "image/png" || article.File.ContentType == "image/jpeg" || article.File.ContentType == "image/jpg")
                {
                    filename = Guid.NewGuid() + article.File.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        article.File.CopyTo(fs);
                    }

                }

                article.PhotoPath = filename;
                article.AddedDate = DateTime.Now;
                article.EditDate = DateTime.Now;

                await _db.Articles.AddAsync(article);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            else
                return View();
        }


        [HttpGet]
        public ActionResult DeleteArticle(int id)
        {
            string path;
            //Delete article from DB
            using (_db)
            {
                Article article = _db.Articles.Find(id);
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", article.PhotoPath);
                _db.Articles.Remove(article);
                _db.SaveChanges();
            }
            System.IO.File.SetAttributes(path, FileAttributes.Normal);
            System.IO.File.Delete(path);

            //Redirect
            return RedirectToAction(nameof(List));
        }




        // GET: Admin/article/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Declare article
            Article article;

            using (_db)
            {
                //Get the article
                article = _db.Articles.Find(id);

                //Make sure article exists
                if (article == null)
                {
                    return Content("That product does not exist.");
                }
            }

            //Return view with model
            return View(article);
        }

        // POST: Admin/article/id
        [HttpPost]
        public ActionResult Edit(Article article)
        {
            //Get article id
            int id = article.Id;

            //Check model state
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            //Make sure article name is unique


            if (_db.Articles.Where(x => x.Id != id).Any(x => x.Name == article.Name))
            {
                ModelState.AddModelError("", "That product name is taken!");
                return View(article);
            }




            //Update article


            Article art = _db.Articles.Find(id);
            string path;
            string filename;

            if (article.File.FileName != null)
            {
                if (article.File.ContentType == "image/png" || article.File.ContentType == "image/jpeg" || article.File.ContentType == "image/jpg")
                {
                    filename = Guid.NewGuid() + article.File.FileName;
                    string pathh = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filename);
                    using (FileStream fs = new FileStream(pathh, FileMode.Create))
                    {
                        article.File.CopyTo(fs);
                    }
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", art.PhotoPath);
                    System.IO.File.Delete(path);
                    art.PhotoPath = filename;

                }
            }

            art.Name = article.Name;
            art.EditDate = DateTime.Now;
            art.Description = article.Description;
            _db.SaveChanges();


            //Set TempData message
            TempData["SM"] = "You have edited the product!";


            //Redirect
            return RedirectToAction(nameof(List));
        }

    }
}