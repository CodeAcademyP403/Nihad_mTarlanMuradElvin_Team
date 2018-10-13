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
    public class RightViewComponent : ViewComponent
    {
        private readonly BlogezyDbContext _db;

        public RightViewComponent(BlogezyDbContext db)
        {
            _db = db;
        }

        //convention based configuration
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SocialAccount> socialaccounts = await _db.SocialAccounts.ToListAsync();
   
            return View(socialaccounts);

        }
    }
}


