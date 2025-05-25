using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class CreateTaskModelwithId
    {
        public string Id { get; set; }
        public string Task { get; set; }

        public string DataTime { get; set; }

        public int CategoryId { get; set; }

        public bool IsCompleted { get; set; }

    }

 
}

