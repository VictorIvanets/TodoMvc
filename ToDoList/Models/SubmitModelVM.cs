using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class SubmitModelVM
    {
        public int id { get; set; }
        public string MyTask { get; set; }

        public DateTime DueDate { get; set; }
        public string DueDateString { get; set; }

        public int CategoryId { get; set; }


        //public TaskModel submitTask { get; set; }
        public List<TaskModel> allTaskNow { get; set; }
        public List<TaskModel> allTaskPast { get; set; }
        public List<TaskModel> allTaskFuture { get; set; }

        public List<SelectListItem> Category { get; set; }
        //public List<CategoryModel> Category { get; set; }

    }
}
