using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
         Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser currentUser);
         
         Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser currentUser);

         Task<bool> MarkDoneAsync(Guid id, ApplicationUser currentUser);
    }
}