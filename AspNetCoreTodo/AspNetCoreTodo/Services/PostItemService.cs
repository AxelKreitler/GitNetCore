using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class PostItemService : IPostItemService
    {
        private readonly ApplicationDbContext _context;

        public PostItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PostItem[]> GetCompletePostsAsync()
        {
            return await _context.Posts.ToArrayAsync();
        }

        public async Task<bool> AddPostAsync(PostItem newPost, ApplicationUser user)
        {
            newPost.Id = Guid.NewGuid();
            newPost.UserId = user.Id;
            newPost.TimeOfCreation = DateTimeOffset.Now;

            _context.Posts.Add(newPost);
            
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<PostItem[]> Search(string searchString)
        {
            return await _context.Posts
                .Where(x => x.Title.Contains(searchString))
                .ToArrayAsync();
        }
    }
}