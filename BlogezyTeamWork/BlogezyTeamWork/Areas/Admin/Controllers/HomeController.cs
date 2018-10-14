using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogezyTeamWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] 
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
            AppUser User = await _db.Users.Where(x => x.Id == HttpContext.Session.GetString("id")).FirstOrDefaultAsync();
            return View(User);
        }


    }
}