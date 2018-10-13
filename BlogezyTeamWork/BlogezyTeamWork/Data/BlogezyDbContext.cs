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
        public BlogezyDbContext(DbContextOptions<BlogezyDbContext> dbContextOptions) : base(dbContextOptions) {
           
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SocialAccount> SocialAccounts { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<SubMenu> SubMenus { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ArticleComments> ArticleComments { get; set; }
       
    }
}
