using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System;
using ToDoList.Models;
using ToDoList.UseDB;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace ToDoList.Controllers
{
    public class SubmitController : Controller
    {
        private readonly Services Services = new ();
       
        public async Task<IActionResult> Index()
        {

            List<TaskModel> getalltask = await Services.AllTask();
            List<CategoryModel> category = await Services.AllCategory();
            List<SelectListItem> CategorySelectList = new();

            foreach (CategoryModel item in category)
            {
                CategorySelectList.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.id.ToString(),
                });
            }

            List<TaskModel> TaskNow = new();
            List<TaskModel> TaskPast = new();
            List<TaskModel> TaskFuture = new();

            foreach (var item in getalltask)
            {
                if (item.DueTimeSpan.TotalMinutes < 0)
                    TaskPast.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 120)
                    TaskFuture.Add(item);
                else if(item.DueTimeSpan.TotalMinutes > 0 && item.DueTimeSpan.TotalMinutes < 120)
                    TaskNow.Add(item);
            }

            TaskPast.Reverse();

            SubmitModelVM init = new(){
                DueDate = DateTime.Now,
                Category = CategorySelectList, 
                allTaskNow = TaskNow, 
                allTaskFuture = TaskFuture,
                allTaskPast = TaskPast,
            };

            return View(init);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? id)
        {

            List<TaskModel> getalltask = await Services.AllTask();
            List<CategoryModel> category = await Services.AllCategory();
            List<SelectListItem> CategorySelectList = new();

            foreach (CategoryModel item in category)
            {
                CategorySelectList.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.id.ToString(),
                });
            }

            List<TaskModel> TaskNow = new();
            List<TaskModel> TaskPast = new();
            List<TaskModel> TaskFuture = new();

            foreach (var item in getalltask)
            {
                if (item.DueTimeSpan.TotalMinutes < 0)
                    TaskPast.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 120)
                    TaskFuture.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 0 && item.DueTimeSpan.TotalMinutes < 120)
                    TaskNow.Add(item);
            }

            TaskPast.Reverse();

            SubmitModelVM init = new()
            {
                Category = CategorySelectList,
                allTaskNow = TaskNow,
                allTaskFuture = TaskFuture,
                allTaskPast = TaskPast,
            };


            if (id != null)
            {
                TaskModel oneTaskById = await Services.GetOne((int)id);
                init.id = oneTaskById.id;
                init.MyTask = oneTaskById.MyTask;
                init.CategoryId = oneTaskById.CategoryId;
                init.DueDate = oneTaskById.DueDate;
                init.DueDateString = DateTime.ParseExact(oneTaskById.DueDate.ToString(), "dd.MM.yyyy HH:mm:ss", null).ToString("yyyy-MM-ddTHH:mm:ss"); 
            }

            return View(init);
        }


        [HttpPost]
        public async Task<IActionResult> AddTask(SubmitModelVM model)
        {
            if (model.id != 0)
            {
                await Services.UpdateTask(model.id, model.MyTask, model.DueDate.ToString(), model.CategoryId);
                return RedirectToAction("Index");
            }
            await Services.AddTask(model.MyTask, model.DueDate.ToString(), model.CategoryId);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> DeleteTask(int id)
        {
            await Services.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}