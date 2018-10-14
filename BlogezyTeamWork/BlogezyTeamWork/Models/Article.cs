﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models
{
    public class Article
    {

        public Article()
        {
            ArticleCategory = new HashSet<ArticleCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Detail { get; set; }
        public string PhotoPath { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime EditDate { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [NotMapped]
        public IFormFile File { get; set; }

        public virtual ICollection<ArticleCategory> ArticleCategory { get; set; }
        public virtual ICollection<ArticleUserComments> ArticleComments { get; set; }
    }
}
