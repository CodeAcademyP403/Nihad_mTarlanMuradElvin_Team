using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models
{
    public class Menu : MenuItem
    {
        public Menu()
        {
            SubMenus = new HashSet<SubMenu>();
        }

        public string Slug { get; set; }
        public int Sorting { get; set; }
        public bool Visibility { get; set; }

        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}
