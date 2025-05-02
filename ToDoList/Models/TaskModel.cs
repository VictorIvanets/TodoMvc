using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskModel
    {
        public int id { get; set; }
        public string MyTask { get; set; }

        public DateTime DueDate { get; set; }

        public TimeSpan DueTimeSpan { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsCompleted { get; set; }

    }
}
