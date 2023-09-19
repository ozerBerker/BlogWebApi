using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlogWebApi.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly Context context;
        public CategoryService(Context context)
        {
            this.context = context;
        }

        public Task<List<Category>> GetCategories()
        {
            var categories = context.Categories.ToListAsync();
            if (categories == null)
                throw new BadRequestException("Something goes wrong and categories can not found!");

            return categories;
        }

        public async Task<Category> Create(CategoryModel paramCategory)
        {
            Category create = new Category()
            {
                CategoryName = paramCategory.CategoryName,
                CategoryIsActive = true,
            };

            var result = await context.AddAsync(create);
            if (result.State != Microsoft.EntityFrameworkCore.EntityState.Added)
                throw new BadRequestException("Something goes wrong and category can not added!");

            await context.SaveChangesAsync();
            return create;
        }

        public async Task<Category> Read(int id)
        {
            var read = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (read == null)
                throw new BadRequestException("Something goes wrong and category can not read!");

            return read;
        }

        public async Task<bool> Update(int categoryId, CategoryModel paramCategory)
        {
            var update = await Read(categoryId);
            if (update == null)
                throw new BadRequestException("Something goes wrong and article can not update!");

            update.CategoryName = paramCategory.CategoryName;

            context.Update<Category>(update);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int categoryId)
        {
            var delete = await Read(categoryId);
            if (delete == null)
                throw new BadRequestException("Something goes wrong and article can not delete!");

            //context.Categories.Remove(delete);
            delete.CategoryIsActive = false;
            context.Update<Category>(delete);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
