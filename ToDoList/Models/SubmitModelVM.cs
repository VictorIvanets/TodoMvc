using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class SubmitModelVM
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Please enter a task")]
        public string MyTask { get; set; }

        [Required(ErrorMessage = "Please enter a date")]
        public DateTime DueDate { get; set; }
   

        [Required(ErrorMessage = "Select category")]
        public int CategoryId { get; set; }


        public string? DueDateString { get; set; }
        public List<TaskModel>? allTaskNow { get; set; }
        public List<TaskModel>? allTaskPast { get; set; }
        public List<TaskModel>? allTaskFuture { get; set; }
        public List<SelectListItem>? Category { get; set; }
    }
}
