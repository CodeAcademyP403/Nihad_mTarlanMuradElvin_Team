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
    public class MenuViewComponent : ViewComponent
    {
        private readonly BlogezyDbContext _db;

        public MenuViewComponent(BlogezyDbContext db)
        {
            _db = db;
        }

        //convention based configuration
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Menu> menus = await _db.Menus.Include(x => x.SubMenus).ToListAsync();

            return View(menus);

        }
    }
}


