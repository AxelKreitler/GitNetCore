using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Controllers
{
    public class PostController
    {
        private readonly IPostItemService _postItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPostItemService postItemService, UserManager<ApplicationUser> userManager)
        {
            _postItemService = postItemService;
            _userManager = userManager;
        }
/*        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);
            
            var model = new TodoViewModel()
            {
                Items = items
            };

            return View(model);
        }
*/
        public async Task<IActionResult> Index(string searchString)
        {

            var posts = await _postItemService.GetCompletePostsAsync();

            if(searchString != null){
                posts = await _postItemService.Search(searchString);
            }

            var model = new PostViewModel()
            {
                Posts = posts
            };

            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(PostItem newPost)
        {
            //https://stackoverflow.com/questions/48915527/how-to-upload-image-in-specified-folder-and-save-path-in-database-net-core
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
                
            var successful = await _postItemService.AddItemAsync(newPost, currentUser);

            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }
    }
}