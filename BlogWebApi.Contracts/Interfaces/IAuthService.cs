using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Contracts.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthenticationResponseDTO> Login(UserSignInModel paramUser);
        public Task<AppUser> Register(UserSignUpModel paramUser);
        public AccessTokenModel CreateAccessToken(AppUser user);
    }
}
