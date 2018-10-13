using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models.ViewModels
{
    public class HomeIndexModel
    {
        public IEnumerable<Article> Articles { get; set; } 
        public IEnumerable<MenuItem> Menus { get; set; }
        public IEnumerable<SocialAccount> SocialAccounts { get; set; }
    }
}
