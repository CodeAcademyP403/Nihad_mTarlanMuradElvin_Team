using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models
{
    public class SubMenu : MenuItem
    {
        public virtual Menu Menu { get; set; }
        public int MenuId { get; set; }
    }
}
