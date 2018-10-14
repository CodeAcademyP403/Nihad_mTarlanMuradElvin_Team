using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Components
{
    public class PopularPostViewComponent:ViewComponent
    {
        private BlogezyDbContext _db;
        public PopularPostViewComponent(BlogezyDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Article> popularArticles = await _db.Articles.Where(a => a.ViewCount >= 1).ToListAsync();
            return View(popularArticles);
        }
    }
}
