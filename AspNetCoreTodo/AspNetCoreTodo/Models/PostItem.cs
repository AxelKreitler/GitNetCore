using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class PostItem
    {
        public Guid Id { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        public string FilePath { get; set; }

        public DateTimeOffset? TimeOfCreation { get; set; }

        public string UserId { get; set; }
        
    }
}
