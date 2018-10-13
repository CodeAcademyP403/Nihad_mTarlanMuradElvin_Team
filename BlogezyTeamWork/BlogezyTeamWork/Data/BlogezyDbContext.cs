using BlogezyTeamWork.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlogezyTeamWork.Data
{
    public class BlogezyDbContext : IdentityDbContext<AppUser>
    {
        public BlogezyDbContext(DbContextOptions<BlogezyDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SubMenu> SubMenus { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ArticleComments> ArticleComments { get; set; }

    }
}
