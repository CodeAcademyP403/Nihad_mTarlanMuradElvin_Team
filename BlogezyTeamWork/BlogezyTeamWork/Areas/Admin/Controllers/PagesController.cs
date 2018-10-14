using System;
using System.Collections.Generic;
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
    public class PagesController : Controller
    {
            private BlogezyDbContext _db;
            public PagesController(BlogezyDbContext db)
            {
                _db = db;
            }


            [HttpGet]
            public IActionResult List()
            {
                //Declare a list of menu
                List<Menu> Menus;

                using (_db)
                {
                    //Init the list
                    Menus = _db.Menus.OrderBy(x => x.Sorting).ToList();
                }

                //Return view with list
                return View(Menus);
            }

            [HttpGet]
            public IActionResult Add()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Add(Menu menu)
            {
                if (ModelState.IsValid)
                {
                    await _db.Menus.AddAsync(menu);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                else
                    return View();
            }


            [HttpGet]
            public async Task<ActionResult> DeleteMenu(int id)
            {
                Menu menu = await _db.Menus.Where(m => m.Id == id).FirstOrDefaultAsync();
                _db.Menus.Remove(menu);
                await _db.SaveChangesAsync();
                //Redirect
                return RedirectToAction(nameof(List));
            }


            // POST: Admin/Shop/ReorderCategories
            [HttpPost]
            public void ReorderMenus(int[] id)
            {
                using (_db)
                {
                    //Set initial count
                    int count = 1;

                    //Declare CategoryDTO
                    Menu menu;

                    //Set sorting for each category
                    foreach (var catId in id)
                    {
                        menu = _db.Menus.Find(catId);
                        menu.Sorting = count;

                        _db.SaveChanges();

                        count++;
                    }
                }
            }

            // GET: Admin/Menu/id
            [HttpGet]
            public ActionResult Edit(int id)
            {

                Menu menu;

                using (_db)
                {
                    //Get the article
                    menu = _db.Menus.Find(id);

                    //Make sure article exists
                    if (menu == null)
                    {
                        return Content("That category does not exist.");
                    }
                }

                //Return view with model
                return View(menu);
            }

            // POST: Admin/Menu/id
            [HttpPost]
            public ActionResult Edit(Menu menu)
            {
                //Get menu id
                int id = menu.Id;

                //Check model state
                if (!ModelState.IsValid)
                {
                    return View(menu);
                }

                //Make sure menu name is unique


                if (_db.Menus.Where(x => x.Id != id).Any(x => x.Name == menu.Name))
                {
                    ModelState.AddModelError("", "That category name is taken!");
                    return View(menu);
                }

                Menu menu_ = _db.Menus.Find(id);


                menu_.Name = menu.Name;
                menu_.Controller = menu.Controller;
                menu_.Action = menu.Action;
                menu_.Visibility = menu.Visibility;

                _db.SaveChanges();


                //Set TempData message
                TempData["SM"] = "You have edited the menu!";


                //Redirect
                return RedirectToAction(nameof(List));
            }
        }
    }
