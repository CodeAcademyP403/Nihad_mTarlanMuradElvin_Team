using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogezyTeamWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogezyDbContext _db;

        public HomeController(BlogezyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _db.Articles.OrderByDescending(x => x.AddedDate)
                                                              .ToListAsync();

            var socialaccounts = await _db.SocialAccounts
                                                        .ToListAsync();

            HomeIndexModel him = new HomeIndexModel
            {
                Articles = articles,
                SocialAccounts = socialaccounts
            };

            return View();

        }
    }
}