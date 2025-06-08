using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class CreateTaskModel
    {
        public int Id { get; set; } = 0!;

        public required string Task { get; set; }
 
        public required string DataTime { get; set; }
  
        public int CategoryId { get; set; }

        public bool IsCompleted { get; set; }
    }
}
