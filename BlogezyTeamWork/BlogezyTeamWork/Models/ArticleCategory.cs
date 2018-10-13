using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        public Article Article { get; set; }
        public int ArticleId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
