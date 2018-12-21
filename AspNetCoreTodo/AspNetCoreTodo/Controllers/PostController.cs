using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AspNetCoreTodo.Controllers
{
    public class PostController : Controller
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
        public async Task<IActionResult> AddPost(PostItem newPost, List <IFormFile> files)
        {
            
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            foreach (var item in files)
            {
                if(item.Length > 0){
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        newPost.files = stream.ToArray();
                        string imgData = Convert.ToBase64String(newPost.files);
                        //Console.WriteLine(imgData);
                        newPost.FilePath = string.Format("data:image/png;base64,{0}", imgData);
                    }
                }
                
                var successful = await _postItemService.AddPostAsync(newPost, currentUser);

                if (!successful)
                {
                    return BadRequest("Could not add item.");
                }
            }
            /*
            if(newPost.FilePath != null){
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(newPost.FilePath);
                var filedir = "wwwroot/images/";
                using (var stream = new FileStream(Path.Combine(filedir, fileName), FileMode.Create))
                {
                    newPost.FilePath = fileName;
                    var successful = await _postItemService.AddPostAsync(newPost, currentUser);

                    if (!successful)
                    {
                        return BadRequest("Could not add item.");
                    }
                }
            }
            */
            
            return RedirectToAction("Index");
        }
    }
}