using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public IActionResult Article(int id)
        {

            Article article = _db.Articles.Include(x => x.ArticleComments).Where(x=>x.Id == id).FirstOrDefault();
            article.ViewCount++;
            _db.SaveChanges();
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(int id)
        {
            if (ModelState.IsValid)
            {
                //var username = HttpContext.Session.GetString("name");
                //AppUser user = await _db.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
                //Article article = await _db.Articles.Where(x => x.Id == id).FirstOrDefaultAsync();
                var commentText = HttpContext.Request.Form["comment-input"].ToString();
                Comment cm = new Comment()
                {
                    Text = commentText,
                    DateAdded = DateTime.Now
                };

                await _db.Comments.AddAsync(cm);
                await _db.SaveChangesAsync();

                ArticleUserComments auc = new ArticleUserComments()
                {
                    //AppUser = user,
                    AppUserId = HttpContext.Session.GetString("id"),
                    //Comment = cm,
                    CommentId = cm.Id,
                    //Article = article,
                    ArticleId = id
                };
                await _db.ArticleUserComments.AddAsync(auc);
                await _db.SaveChangesAsync();

                Article article = await _db.Articles.FindAsync(id);
                article.CommentCount++;
                await _db.SaveChangesAsync();

                return RedirectToAction("Article", "Home", new { Id = id });
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DeleteComment(int id)
        {
            int artId;
            //Delete comment from DB
            using (_db)
            {
                ArticleUserComments auc = await _db.ArticleUserComments.Where(x => x.CommentId == id).FirstOrDefaultAsync();
                artId = auc.ArticleId;


                Article article = await _db.Articles.FindAsync(auc.ArticleId);
                article.CommentCount--;
                await _db.SaveChangesAsync();

                _db.ArticleUserComments.Remove(auc);

                _db.SaveChanges();

                Comment comment = await _db.Comments.FindAsync(id);

                _db.Comments.Remove(comment);

                

                _db.SaveChanges();
            }

            //Redirect
            return RedirectToAction("Article", "Home", new { Id = artId });
        }
    }
}