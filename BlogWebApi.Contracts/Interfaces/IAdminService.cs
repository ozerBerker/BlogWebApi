using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Contracts.Interfaces
{
    public interface IAdminService
    {
        public Task<List<AppUser>> GetUsers();
        //public Task<AppUser> Create(AppUser user);
        public Task<AppUser> Read(int id);
        public Task<bool> Update(int id, AppUserModel user);
        public Task<bool> Delete(int id);
        public Task<bool> AddRoleToUser (int id, string role);
        public Task<bool> TakeOutRolesFromUser(int id, IEnumerable<string> roles);
    }
}
