using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly Service _Service;


        public ToDoListController(IToDoServiceFactory factory)
        {
            _Service = factory.GetService();
        }

        public IActionResult Index()
        {
            List<TaskModel> getalltask = _Service.AllTask();
            List<CategoryModel> category = _Service.GetAllCategory();
            List<SelectListItem> categorySelectList = new();

            foreach (CategoryModel item in category)
            {
                categorySelectList.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.Id.ToString(),
                });
            }

            List<TaskModel> taskNow = new();
            List<TaskModel> taskPast = new();
            List<TaskModel> taskFuture = new();

            foreach (var item in getalltask)
            {
                if (item.IsCompleted)
                    taskPast.Add(item);
                else if (item.DueTimeSpan.TotalMinutes < 120 && !item.IsCompleted)
                    taskNow.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 120 && !item.IsCompleted)
                    taskFuture.Add(item);
            }

            taskPast.Reverse();
            taskNow.Reverse();


            ToDoListModelVM init = new()
            {
                Store = StoreXML.GetStorage() ? "SQL" : "XML",
                DueDate = DateTime.Now,
                Category = categorySelectList,
                AllTaskNow = taskNow,
                AllTaskFuture = taskFuture,
                AllTaskPast = taskPast,
            };
            return View(init);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int? id)
        {

            List<TaskModel> getalltask = _Service.AllTask();
            List<CategoryModel> category = _Service.GetAllCategory();
            List<SelectListItem> categorySelectList = new();

            foreach (CategoryModel item in category)
            {
                categorySelectList.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.Id.ToString(),
                });
            }

            List<TaskModel> taskNow = new();
            List<TaskModel> taskPast = new();
            List<TaskModel> taskFuture = new();

            foreach (var item in getalltask)
            {

                if (item.IsCompleted)
                    taskPast.Add(item);
                else if (item.DueTimeSpan.TotalMinutes < 120 && !item.IsCompleted)
                    taskNow.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 120 && !item.IsCompleted)
                    taskFuture.Add(item);
            }

            taskPast.Reverse();
            taskNow.Reverse();

            ToDoListModelVM init = new()
            {
                Category = categorySelectList,
                AllTaskNow = taskNow,
                AllTaskFuture = taskFuture,
                AllTaskPast = taskPast,
            };

            if (id != null)
            {
                TaskModel oneTaskById = _Service.GetOne((int)id);
                init.Id = oneTaskById.Id;
                init.MyTask = oneTaskById.MyTask;
                init.CategoryId = oneTaskById.CategoryId;
                init.DueDate = oneTaskById.DueDate;
                init.DueDateString =
                    DateTime.ParseExact(oneTaskById.DueDate.ToString(), "dd.MM.yyyy HH:mm:ss", null)
                    .ToString("yyyy-MM-ddTHH:mm:ss");
            }

            return View(init);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(ToDoListModelVM model)
        {
            if (ModelState.IsValid)
            {
                TimeSpan dueTimeSpan = model.DueDate - DateTime.Now;
                string dataActual = dueTimeSpan.TotalMinutes < 0
                    ? DateTime.Now.ToString()
                    : model.DueDate.ToString();

                CreateTaskModel newTask = new()
                {
                    Task = model.MyTask,
                    DateTime = dataActual,
                    CategoryId = model.CategoryId,
                    IsCompleted = false,
                };
                if (model.Id != 0)
                {
                    _Service.UpdateTask(model.Id, newTask);
                    return RedirectToAction("Index");
                }
                _Service.AddTask(newTask);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetIsCompleted(int id)
        {
            _Service.IsCompleted(id, DateTime.Now.ToString());
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetService(int id)
        {
            StoreXML.SetStorage(id);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int id)
        {
            _Service.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}