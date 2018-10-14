using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlogezyTeamWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();

            using (IServiceScope scopedService = webHost.Services.CreateScope())
            {
                using (BlogezyDbContext dbContext = scopedService.ServiceProvider.GetRequiredService<BlogezyDbContext>())
                {
                    if (!dbContext.Articles.Any())
                    {
                        #region default articles
                        Article article = new Article()
                        {
                            Name = "Beautiful Day With Friends In Paris",
                            Description = "Whether an identity or campaign, we make your brand visible, relevant and effective by placing the digital at the center of its ecosystem, without underestimating the power of traditional media. Whether an identity or campaign, we make your brand visible.",
                            Detail = "Whether an identity or campaign, we make your brand visible, relevant and effective by placing the digital at the center of its ecosystem, without underestimating the power of traditional media. Whether an identity or campaign, we make your brand visible.",
                            PhotoPath = "blog-1.jpg",
                            AddedDate = DateTime.Now,
                            EditDate = DateTime.Now,
                            ViewCount = 0,
                            CommentCount = 0
                        };

                        #endregion

                        #region default categories
                        Category branding = new Category()
                        {
                            Name = "Branding",
                            ArticleCount = 0
                        };

                        Category design = new Category()
                        {
                            Name = "Design",
                            ArticleCount = 0
                        };

                        #endregion

                        dbContext.Articles.Add(article);
                        dbContext.Categories.AddRange(branding, design);
                        dbContext.SaveChanges();

                        ArticleCategory ac1 = new ArticleCategory()
                        {
                            ArticleId = article.Id,
                            CategoryId = branding.Id
                        };

                        ArticleCategory ac2 = new ArticleCategory()
                        {
                            ArticleId = article.Id,
                            CategoryId = design.Id
                        };

                        dbContext.ArticleCategories.AddRange(ac1, ac2);
                        dbContext.SaveChanges();
                    }


                    if (!dbContext.Menus.Any())
                    {
                        #region default menus
                        Menu home = new Menu()
                        {
                            Name = "Home",
                            Slug = "home",
                            Sorting = 1,
                            Visibility = true,
                            Controller = "Home",
                            Action = "Index"
                        };

                        Menu features = new Menu()
                        {
                            Name = "Features",
                            Slug = "features",
                            Sorting = 2,
                            Visibility = true
                        };

                        #endregion

                        dbContext.Menus.AddRange(home, features);
                        dbContext.SaveChanges();

                        #region sub menus
                        SubMenu subCategoryStandartPost = new SubMenu()
                        {
                            Action = "StandartPost",
                            Controller = "Home",
                            Name = "Standart post",
                            MenuId = features.Id
                        };

                        SubMenu subCategoryVideoPost = new SubMenu()
                        {
                            Action = "VideoPost",
                            Controller = "Home",
                            Name = "Video post",
                            MenuId = features.Id
                        };
                        #endregion

                        dbContext.SubMenus.AddRange(subCategoryVideoPost, subCategoryStandartPost);
                        dbContext.SaveChanges();


                    }

                    if (!dbContext.SocialAccounts.Any())
                    {
                        #region default menus
                        SocialAccount facebok = new SocialAccount()
                        {
                            Name = "Facebook",
                            Url = "facebook.com",
                            Icon = "fab fa-facebook-f"

                        };

                        SocialAccount twitter = new SocialAccount()
                        {
                            Name = "Twitter",
                            Url = "twitter.com",
                            Icon = "fab fa-twitter" 

                        };

                        SocialAccount instagram = new SocialAccount()
                        {
                            Name = "Instagram",
                            Url = "instagram.com",
                            Icon = "fab fa-instagram"

                        };

                        SocialAccount pinterest = new SocialAccount()
                        {
                            Name = "Pinterest",
                            Url = "pinterest.com",
                            Icon = "fab fa-pinterest-p"

                        };

                        #endregion

                        dbContext.SocialAccounts.AddRange(facebok, twitter, instagram, pinterest);
                        dbContext.SaveChanges();

                      
                    }

                    if (!dbContext.AdminInfos.Any())
                    {
                        AdminInfo adminInfo = new AdminInfo()
                        {
                            Name = "Murad",
                            Surname = "Ibrahimkhanli",
                            About="Pul pres Heyat Impres!!!",
                            PhotoPath = "admin.jpg"
                        };
                        dbContext.AdminInfos.Add(adminInfo);
                        dbContext.SaveChanges();
                    }
                    UserAndRoleCreater.CreateAsync(scopedService, dbContext).Wait();

                }
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
