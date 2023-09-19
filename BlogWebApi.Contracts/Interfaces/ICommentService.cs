using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Contracts.Interfaces
{
    public interface ICommentService
    {
        public Task<List<Comment>> GetComments();
        public Task<Comment> Create(CommentModel comment, int userId, int articleId);
        public Task<Comment> Read(int id);
        public Task<bool> Update(int id, CommentModel comment, int userId);
        public Task<bool> Delete(int id, int userId);
    }
}
