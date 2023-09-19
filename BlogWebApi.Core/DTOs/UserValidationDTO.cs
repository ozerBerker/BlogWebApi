using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.DTOs
{
    public class UserValidationDTO
    {
        public int userId { get; set; }
        public IList<string> userRoles { get; set; }
    }
}
    