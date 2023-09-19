using BlogWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.Models
{
    public class UserSignUpModel
    {
        //[Display(Name ="Ad Soyad")]
        //[Required(ErrorMessage ="Lütfen Ad-Soyad bilgilerinizi giriniz.")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        public string UserName{ get; set; }

        [Required]
        public string UserFirstName { get; set; }

        [Required]
        public string UserLastName { get; set; }

        [Required]
        public string UserPassword { get; set; }

    }
}
