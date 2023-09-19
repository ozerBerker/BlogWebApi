using BlogWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.Models
{
    public class ArticleModel
    {
        public string ArticleTitle { get; set; }
        public string? ArticleContent { get; set; }
        public string? ArticleImage { get; set; }
        public bool? ArticleIsActive { get; set; }
        public int? CategoryId { get; set; }
    }
}
