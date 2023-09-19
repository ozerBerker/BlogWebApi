using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using BlogWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BlogWebApi.Core.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.SecurityTokenService;
using System.Security.Authentication;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlogWebApi.Services.Services
{
    public class ArticleService : IArticleService
    {
        private readonly Context context;
        private readonly IUserService userService;
        public ArticleService(Context context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<List<Article>> GetArticles()
        {
            var articles = await context.Articles.ToListAsync();
            if (articles == null)
                throw new BadRequestException("Something goes wrong and articles can not found!");

            return articles;
        }

        public async Task<Article> Create(ArticleModel paramArticle, int userId)
        {
            Article create = new Article()
            {
                ArticleTitle = paramArticle.ArticleTitle,
                ArticleContent = paramArticle.ArticleContent,
                ArticleImage = paramArticle.ArticleImage,
                ArticleCreateDate = DateTime.Now,
                ArticleIsActive = true,
                CategoryId = paramArticle.CategoryId,
                UserId = userId
            };

            var result = await context.AddAsync(create);
            if (result.State != Microsoft.EntityFrameworkCore.EntityState.Added)
                throw new BadRequestException("Something goes wrong and article can not added!");

            await context.SaveChangesAsync();
            return create;
        }

        public async Task<Article> Read(int id)
        {
            var read = await context.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);
            if (read == null)
                throw new BadRequestException("Something goes wrong and article can not read!");

            return read;
        }


        public async Task<bool> Update(int articleId, ArticleModel paramArticle, int userId)
        {
            var user = await userService.GetUserDTO(userId);
            if (user == null)
                throw new AuthenticationException("You must login and valid to delete an article!");
            //throw new Exception("You must login and valid to delete an article!");

            var update = await Read(articleId);
            if (update == null)
                throw new BadRequestException("Something goes wrong and article can not update!");

            var isVerified = isUserVerified(update.UserId, user);
            if (!isVerified)
                throw new UnauthorizedAccessException("You do not have a permission to delete this article!");

            /*
             * Update article row according to parameter
             string ArticleTitle 
             string ArticleContent 
             string ArticleImage 
             bool ArticleIsActive 
             int CategoryId 
             */

            update.ArticleTitle = paramArticle.ArticleTitle;
            update.ArticleContent = paramArticle.ArticleContent;
            update.ArticleImage = paramArticle.ArticleImage;
            update.CategoryId = paramArticle.CategoryId;


            context.Update<Article>(update);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int articleId, int userId)
        {
            var user = await userService.GetUserDTO(userId);
            if (user == null)
                throw new AuthenticationException("You must login and valid to delete an article!");
            //throw new Exception("You must login and valid to delete an article!");

            var delete = await Read(articleId);
            if (delete == null)
                throw new BadRequestException("Something goes wrong and articleSomething goes wrong and articles can not found can not delete!");


            var isVerified = isUserVerified(delete.UserId, user);
            if (!isVerified)
                throw new UnauthorizedAccessException("You do not have a permission to delete this article!");

            delete.ArticleIsActive = false;
            //var result = context.Articles.Remove(delete);
            context.Update<Article>(delete);
            await context.SaveChangesAsync();
            return true;
        }

        private bool isUserVerified(int userId, UserValidationDTO user)
        {
            //If is it the creator of the article, return true
            if ( userId.Equals(user.userId) || user.userRoles.Contains("Admin") )  return true;
            else return false;
        }
    }
}