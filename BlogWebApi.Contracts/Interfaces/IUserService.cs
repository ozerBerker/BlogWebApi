using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Contracts.Interfaces
{
    public interface IUserService
    {
        public Task<SignInResult> PasswordSignInAsync(UserSignInModel paramUser);
        public Task<AppUser> FindByEmailAsync(string email);
        public Task<AppUser> FindByIdAsync(int id);
        public Task<bool> IsUserRegistered(string email);
        public Task<IdentityResult> CreateAsync(AppUser user, string password);
        public Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
        public Task<IdentityResult> RemoveFromRolesAsync(AppUser user, IEnumerable<string> role);
        public Task<IList<string>> GetRolesAsync(AppUser user);
        public Task<UserValidationDTO> GetUserDTO(int userId);
    }
}
