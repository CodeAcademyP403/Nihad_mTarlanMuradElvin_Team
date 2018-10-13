using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        public virtual Article Article { get; set; }
        public int ArticleId { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
