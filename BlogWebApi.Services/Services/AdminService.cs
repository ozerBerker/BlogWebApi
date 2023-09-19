using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlogWebApi.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly Context context;
        private readonly IUserService userService;
        public AdminService(Context context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<List<AppUser>> GetUsers()
        {
            var users = await context.Users.ToListAsync();
            if (users == null)
                throw new BadRequestException("Something goes wrong and articles can not found!");

            return users;
        }

        public async Task<AppUser> Read(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new BadRequestException("Something goes wrong and article can not read!");

            return user;
        }

        public async Task<bool> Update(int id, AppUserModel user)
        {
            var update  = await Read(id);
            if(update == null)
                throw new BadRequestException("Something goes wrong and user can not found!");

            update.FirstName = user.FirstName;
            update.LastName = user.FirstName;

            context.Update<AppUser>(update);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var delete = await Read(id);
            if (delete == null)
                throw new BadRequestException("Something goes wrong and user can not found!");


            delete.IsActive = false;

            context.Update<AppUser>(delete);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRoleToUser(int id, string role)
        {
            var result = userService.AddToRoleAsync(await Read(id), role);
            if (result == null)
                throw new BadRequestException("Something goes wrong and role can not added to the user!");
            return true;
        }

        public async Task<bool> TakeOutRolesFromUser(int id, IEnumerable<string> roles)
        {
            
            var result = userService.RemoveFromRolesAsync(await Read(id), roles);
            if (result == null)
                throw new BadRequestException("Something goes wrong and role can not added to the user!");
            return true;
        }

    }
}
