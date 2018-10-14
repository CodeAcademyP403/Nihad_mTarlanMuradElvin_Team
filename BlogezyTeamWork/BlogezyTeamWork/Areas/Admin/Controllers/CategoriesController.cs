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
    public class CategoriesController : Controller
    {

        private BlogezyDbContext _db;
        public CategoriesController(BlogezyDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult List()
        {
            //Declare a list of categories
            List<Category> Categories;

            using (_db)
            {
                //Init the list
                Categories = _db.Categories.ToList();
            }

            //Return view with list
            return View(Categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                category.ArticleCount = 0;

                await _db.Categories.AddAsync(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            else
                return View();
        }


        [HttpGet]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            List<ArticleCategory> artcat = await _db.ArticleCategories.Where(x => x.CategoryId == id).ToListAsync();
            _db.ArticleCategories.RemoveRange(artcat);
            await _db.SaveChangesAsync();

            Category category = _db.Categories.Find(id);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();



            //Redirect
            return RedirectToAction(nameof(List));
        }




        // GET: Admin/article/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Declare Social Account
            Category category;

            using (_db)
            {
                //Get the article
                category = _db.Categories.Find(id);

                //Make sure article exists
                if (category == null)
                {
                    return Content("That category does not exist.");
                }
            }

            //Return view with model
            return View(category);
        }

        // POST: Admin/article/id
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            //Get article id
            int id = category.Id;

            //Check model state
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            //Make sure article name is unique


            if (_db.Articles.Where(x => x.Id != id).Any(x => x.Name == category.Name))
            {
                ModelState.AddModelError("", "That category name is taken!");
                return View(category);
            }

            Category cat = _db.Categories.Find(id);


            cat.Name = category.Name;
       

            _db.SaveChanges();


            //Set TempData message
            TempData["SM"] = "You have edited the category!";


            //Redirect
            return RedirectToAction(nameof(List));
        }

    }
}