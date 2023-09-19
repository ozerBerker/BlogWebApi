using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.Models
{
    public class AccessTokenModel
    {
        public string Token { get; set; } // Token Değeri
        public DateTime Expiration { get; set; } // Token geçerlilik süresi
        public IEnumerable<Claim> claims { get; set; } // DELETE LATER
    }
}
