//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BlogezyTeamWork.Data;
//using BlogezyTeamWork.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BlogezyTeamWork.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class HomeController : Controller
//    {
//        private readonly BlogezyDbContext _db;

//        public HomeController(BlogezyDbContext db)
//        {
//            _db = db;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            return View();

//        }


//        [HttpGet]
//        public async Task<IActionResult> Payments()
//        {
//            int userId = CheckConnection();
//            if (userId == 0) return RedirectToAction("Index", "Account");

//            List<Payment> Payments = await _db.Payments.Include(x => x.Game).ToListAsync();

//            if (Payments.LongCount() > 0)
//            {
//                return View(Payments);
//            }
//            else return Content("There are no payments!");

//        }

//        [HttpGet]
//        public async Task<IActionResult> PaymentDetails(int id)
//        {
//            int userId = CheckConnection();
//            if (userId == 0) return RedirectToAction("Index", "Account");

//            //Payment Payment = await _db.Payments.Include(x => x.Game).FindAsync(id);
//            Payment Payment = await _db.Payments.Include(x => x.Game).Where(x => x.Id == id).FirstOrDefaultAsync();
//            return View(Payment);
//        }


//        [HttpGet]
//        public async Task<IActionResult> PaymentConfirm(int id)
//        {
//            int userId = CheckConnection();
//            if (userId == 0) return RedirectToAction("Index", "Account");

//            Payment Payment = await _db.Payments.FindAsync(id);
//            Payment.Status = true;
//            _db.SaveChanges();
//            return RedirectToAction(nameof(Payments));
//        }

//    }
//}