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
    public class SocialAccountsController : Controller
    {

        private BlogezyDbContext _db;
        public SocialAccountsController(BlogezyDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult List()
        {
            //Declare a list of ProductVM
            List<SocialAccount> Accounts;

            using (_db)
            {
                //Init the list
                Accounts = _db.SocialAccounts.ToList();
            }

            //Return view with list
            return View(Accounts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SocialAccount account)
        {
            if (ModelState.IsValid)
            {
                await _db.SocialAccounts.AddAsync(account);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            else
                return View();
        }


        [HttpGet]
        public ActionResult DeleteSA(int id)
        {
            using (_db)
            {
                SocialAccount account = _db.SocialAccounts.Find(id);
                _db.SocialAccounts.Remove(account);
                _db.SaveChanges();
            }


            //Redirect
            return RedirectToAction(nameof(List));
        }




        // GET: Admin/article/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Declare Social Account
            SocialAccount account;

            using (_db)
            {
                //Get the article
                account = _db.SocialAccounts.Find(id);

                //Make sure article exists
                if (account == null)
                {
                    return Content("That account does not exist.");
                }
            }

            //Return view with model
            return View(account);
        }

        // POST: Admin/article/id
        [HttpPost]
        public ActionResult Edit(SocialAccount account)
        {
            //Get article id
            int id = account.Id;

            //Check model state
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            //Make sure article name is unique


            if (_db.Articles.Where(x => x.Id != id).Any(x => x.Name == account.Name))
            {
                ModelState.AddModelError("", "That product name is taken!");
                return View(account);
            }

            SocialAccount sa = _db.SocialAccounts.Find(id);


            sa.Name = account.Name;
            sa.Url = account.Url;
            sa.Icon = account.Icon;

            _db.SaveChanges();


            //Set TempData message
            TempData["SM"] = "You have edited the product!";


            //Redirect
            return RedirectToAction(nameof(List));
        }

    }
}