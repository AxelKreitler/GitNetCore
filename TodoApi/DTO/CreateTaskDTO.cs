using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTO
{
    public class CreateTaskDTO : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results=new List<ValidationResult>();

            if (Name.Equals("sarasa"))
            {
                results.Add(new ValidationResult("Sea serio, y pongaleun nombre como la gente a la tarea.",new[] { "Name" }));
            }
            return results;

        }
    }
}