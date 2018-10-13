using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models
{
    public class ArticleComments
    {
        public int Id { get; set; }
        public Article Article { get; set; }
        public int ArticleId { get; set; }
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
    }
}
