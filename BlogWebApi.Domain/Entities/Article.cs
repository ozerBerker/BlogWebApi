using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Domain.Entities
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string? ArticleTitle { get; set; }
        public string? ArticleContent { get; set; }
        public string? ArticleImage { get; set; }
        public DateTime? ArticleCreateDate { get; set; }
        public bool? ArticleIsActive { get; set; }


        public int? CategoryId { get; set; }
        public int UserId { get; set; }

        //Virtuals
        public virtual List<Comment>? Comments { get; set; }
        public virtual Category? Category { get; set; }

    }
}
