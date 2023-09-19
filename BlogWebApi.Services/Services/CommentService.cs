using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Entities;
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
    public class CommentService : ICommentService
    {
        private readonly Context context;
        private readonly IUserService userService;
        public CommentService(Context context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<List<Comment>> GetComments()
        {
            var comments = await context.Comments.ToListAsync();
            if (comments == null)
                throw new BadRequestException("Something goes wrong and comments can not found!");

            return comments;
        }

        public async Task<Comment> Create(CommentModel paramComment, int userId, int articleId)
        {
            Comment create = new Comment()
            {
                CommentTitle = paramComment.CommentTitle,
                CommentContent = paramComment.CommentContent,
                CommentCreatedDate = DateTime.Now,
                CommentIsActive = true,
                UserId = userId,
                ArticleId = articleId
            };

            var result = await context.AddAsync(create);
            if (result.State != Microsoft.EntityFrameworkCore.EntityState.Added)
                throw new BadRequestException("Something goes wrong and comment can not added!");

            await context.SaveChangesAsync();
            return create;
        }

        public async Task<Comment> Read(int id)
        {
            var read = await context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
            if (read == null)
                throw new BadRequestException("Something goes wrong and comment can not be read!");

            return read;
        }


        public async Task<bool> Update(int commentId, CommentModel paramArticle, int userId)
        {
            var user = await userService.GetUserDTO(userId);
            if (user == null)
                throw new AuthenticationException("You must login and valid to update an comment!");

            var update = await Read(commentId);
            if (update == null)
                throw new BadRequestException("Something goes wrong and comment can not update!");

            var isVerified = isUserVerified(update.UserId, user);
            if (!isVerified)
                throw new UnauthorizedAccessException("You do not have a permission to delete this comment!");

            /*
             * Update article row according to parameter
             string CommentTitle 
             string CommentContent 
             bool CommentIsActive 
             */

            update.CommentTitle = paramArticle.CommentTitle;
            update.CommentContent = paramArticle.CommentContent;


            context.Update<Comment>(update);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int commentId, int userId)
        {
            var user = await userService.GetUserDTO(userId);
            if (user == null)
                throw new AuthenticationException("You must login and valid to delete an comment!");

            var delete = await Read(commentId);
            if (delete == null)
                throw new BadRequestException("Something goes wrong and comment can not delete!");

            var isVerified = isUserVerified(delete.UserId, user);
            if (!isVerified)
                throw new UnauthorizedAccessException("You do not have a permission to delete this comment!");



            //var result = context.Comments.Remove(delete);
            delete.CommentIsActive = false;

            context.Update<Comment>(delete);
            await context.SaveChangesAsync();
            return true;
        }

        private bool isUserVerified(int userId, UserValidationDTO user)
        {
            //If is it the creator of the article, return true
            if (userId.Equals(user.userId) || user.userRoles.Contains("Admin")) return true;
            else return false;
        }
    }
}
