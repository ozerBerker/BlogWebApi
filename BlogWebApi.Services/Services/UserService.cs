using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogWebApi.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }
        public async Task<IdentityResult> RemoveFromRolesAsync(AppUser user, IEnumerable<string> roles)
        {
            return await userManager.RemoveFromRolesAsync(user, roles);
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<AppUser> FindByIdAsync(int id)
        {
            return await userManager.FindByIdAsync(id.ToString());
        }

        public Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsUserRegistered(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            if (result == null) return true; // Never registered
            else return false; //Already registered with same email
        }

        public async Task<SignInResult> PasswordSignInAsync(UserSignInModel user)
        {
            return await signInManager.PasswordSignInAsync(user.UserEmail, user.UserPassword, false, false); 
        }

        public async Task<UserValidationDTO> GetUserDTO(int userId)
        {
            var user = await FindByIdAsync(userId);
            var roles = await GetRolesAsync(user);

            return new UserValidationDTO()
            {
                userId = user.Id,
                userRoles = roles
            };
        }
    }
}
