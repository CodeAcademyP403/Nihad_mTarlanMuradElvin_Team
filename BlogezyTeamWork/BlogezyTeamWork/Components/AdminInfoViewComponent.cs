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
    public class AdminInfoViewComponent:ViewComponent
    {
        private readonly BlogezyDbContext _db;

        public AdminInfoViewComponent(BlogezyDbContext db)
        {
            _db = db;
        }

        //convention based configuration
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<AdminInfo> adminInfos = await _db.AdminInfos.ToListAsync();
            return View(adminInfos);

        }
    }
}
