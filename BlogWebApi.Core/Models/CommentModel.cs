using BlogWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.Models
{
    public class CommentModel
    {
        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }
        public bool CommentIsActive { get; set; }
    }
}
