using BlogWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.DTOs
{
    public class AuthenticationResponseDTO
    {
        public string Token { get; set; } // Token Değeri
        public DateTime Expiration { get; set; } // Token geçerlilik süresi
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public IList<string> UserRole { get; set; }
        public bool UserIsActive { get; set; }
    }
}
