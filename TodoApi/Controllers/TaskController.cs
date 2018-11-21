using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TaskItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTask")]
        public ActionResult<TaskItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(TaskItem item)
        {
            if(item.Status != TaskStatus.Todo)
            {
                return BadRequest("No se puede inicializar una tarea con un estado "+item.Status);
            }
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTask", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TaskItem item)
        {
            var task = _context.TodoItems.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            if(task.Status == TaskStatus.Canceled && item.Status == TaskStatus.Done)
            {
                return BadRequest("No se puede actualizar una tarea cancelada a finalizada");
            }

            task.Status = item.Status;
            task.Name = item.Name;

            _context.TodoItems.Update(task);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}