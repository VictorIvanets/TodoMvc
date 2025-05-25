using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class CreateTaskModel
    {

        public string Task { get; set; }
 
        public string DataTime { get; set; }
  
        public int CategoryId { get; set; }
 
        public bool IsCompleted { get; set; }
    }
}
