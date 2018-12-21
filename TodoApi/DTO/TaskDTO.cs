using System.ComponentModel.DataAnnotations;

namespace TaskApi.DTO
{
    public class TaskDTO
    {
        public int id { get; set; }

        public string Name { get; set; }

        [Range (0,4)]
        public int Status{ get; set; }
    }
}