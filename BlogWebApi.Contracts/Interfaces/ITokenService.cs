using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Contracts.Interfaces
{
    public interface ITokenService
    {
        public AccessTokenModel CreateToken(AppUser user, Task<IList<string>> roles);
    }
}
