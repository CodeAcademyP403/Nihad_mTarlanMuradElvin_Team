using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
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
            //var articles = from m in _db.ArticleCategories
            //               join t in _db.Articles on m.ArticleId equals t.Id
            //               join w in _db.Categories on m.CategoryId equals w.Id
            //               select new 

            var articles = await _db.Articles.Include(x => x.ArticleCategory).OrderByDescending(x => x.AddedDate)
                                                  .ToListAsync();

            //var articles = await _db.Articles.Include(m => m.ArticleCategory).ToListAsync();
            //{ }
            //var name = articles[0].ArticleCategory.FirstOrDefault().Category.Name;

            return View(articles);

        }
    }
}