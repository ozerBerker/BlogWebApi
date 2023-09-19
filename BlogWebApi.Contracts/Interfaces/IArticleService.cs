using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Contracts.Interfaces
{
    public interface IArticleService
    {
        public Task<List<Article>> GetArticles();
        public Task<Article> Create(ArticleModel article, int userId);
        public Task<Article> Read(int id);
        public Task<bool> Update(int id, ArticleModel article, int userId);        
        public Task<bool> Delete(int id, int userId);
        //public Task<UserValidationDTO> GetUserDTO(int userId);
    }
}
