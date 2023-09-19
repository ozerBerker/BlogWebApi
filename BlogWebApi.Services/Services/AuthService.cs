using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        public AuthService(ITokenService tokenService, IUserService userService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
        }

        public async Task<AuthenticationResponseDTO> Login(UserSignInModel paramUser)
        {
            var user = await userService.FindByEmailAsync(paramUser.UserEmail);
            if (user == null)
                throw new BadRequestException($"User not be found");

            var result = await userService.PasswordSignInAsync(paramUser);
            if (!result.Succeeded)
                throw new BadRequestException("Something goes wrong!");

            var accessToken = CreateAccessToken(user);

            AuthenticationResponseDTO dto = new AuthenticationResponseDTO()
            {
                //Token = accessToken.Token,
                Token = "Bearer " + accessToken.Token,
                Expiration = accessToken.Expiration,
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                UserEmail = user.Email,
                UserIsActive = user.IsActive,
                UserRole = userService.GetRolesAsync(user).Result
            };

            return dto;
        }

        public async Task<AppUser> Register(UserSignUpModel paramUser)
        {
            var isUserRegistered = await userService.IsUserRegistered(paramUser.UserEmail); ;
            if (!isUserRegistered)
                throw new BadRequestException("Email Already registered.");

            AppUser user = new AppUser()
            {
                Email = paramUser.UserEmail,
                UserName = paramUser.UserName,
                FirstName = paramUser.UserFirstName,
                LastName = paramUser.UserLastName,
                PasswordSalt = paramUser.UserPassword,
                IsActive = true,
                CreatedDate = DateTime.Now,
            };

            var result = await userService.CreateAsync(user, paramUser.UserPassword);
            if (result.Succeeded)
            {
                var role = await userService.AddToRoleAsync(user, "Basic");
                return user;
            }
            else throw new BadRequestException("Something goes wrong!");
        }

        public AccessTokenModel CreateAccessToken(AppUser user)
        {
            //var claims = userService.GetClaims(user);
            //var claims = userService.GetClaimsAsync(user);
            var roles = userService.GetRolesAsync(user);
            var accessToken = tokenService.CreateToken(user, roles);
            return accessToken;
        }
    }
}
