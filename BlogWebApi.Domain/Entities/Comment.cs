using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Domain.Entities
{
    public class Comment
    {

        [Key]
        public int CommentId { get; set; }
        public string? CommentTitle { get; set; }
        public string? CommentContent { get; set; }
        public DateTime? CommentCreatedDate { get; set; }
        public bool? CommentIsActive { get; set; }


        public int UserId { get; set; }
        public int ArticleId { get; set; }

        //Virtuals
        // public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Article Article { get; set; }


    }
}
