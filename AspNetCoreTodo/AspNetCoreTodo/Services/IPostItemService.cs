using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface IPostItemService
    {
         Task<PostItem[]> GetCompletePostsAsync();
         
         Task<bool> AddPostAsync(PostItem newPost, ApplicationUser currentUser);

         Task<PostItem[]> Search(string searchString);
    }
}