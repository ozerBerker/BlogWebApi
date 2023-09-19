using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.Models
{
    public class CategoryModel
    {
        public string CategoryName { get; set; }
        public bool? CategoryIsActive { get; set; }
    }
}
