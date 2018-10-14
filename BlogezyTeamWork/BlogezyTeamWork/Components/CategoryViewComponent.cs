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
    public class CategoryViewComponent:ViewComponent
    {
        private readonly BlogezyDbContext _db;

        public CategoryViewComponent(BlogezyDbContext db)
        {
            _db = db;
        }

        //convention based configuration
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = await _db.Categories.ToListAsync();

            for (int i = 0; i < categories.Count(); i++)
            {
                IEnumerable<ArticleCategory> cat = await _db.ArticleCategories.Where(x => x.CategoryId == categories[i].Id).ToListAsync();
                categories[i].ArticleCount = cat.Count();
               
            }

            return View(categories);

        }
    }
}
