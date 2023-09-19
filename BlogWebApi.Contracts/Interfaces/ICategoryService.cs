using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;

namespace BlogWebApi.Contracts.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategories();
        public Task<Category> Create(CategoryModel category);
        public Task<Category> Read(int id);
        public Task<bool> Update(int categoryId, CategoryModel category);
        public Task<bool> Delete(int id);
    }
}
