using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly Service _Service;

        public ToDoListController()
        {
            _Service = StoreXML.GetStorage() ? new SQLService() : new XMLService();
        }

        public IActionResult Index()
        {
            ViewData["DB"] = StoreXML.GetStorage() ? "SQL" : "XML";

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
                if (item.DueTimeSpan.TotalMinutes < 0 || item.IsCompleted)
                    taskPast.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 120 && !item.IsCompleted)
                    taskFuture.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 0 && item.DueTimeSpan.TotalMinutes < 120 && !item.IsCompleted)
                    taskNow.Add(item);
            }

            taskPast.Reverse();

            ToDoListModelVM init = new()
            {
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
            List<SelectListItem> CategorySelectList = new();

            foreach (CategoryModel item in category)
            {
                CategorySelectList.Add(new SelectListItem
                {
                    Text = item.CategoryName,
                    Value = item.Id.ToString(),
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
                else if (item.DueTimeSpan.TotalMinutes > 0
                    && item.DueTimeSpan.TotalMinutes < 120)

                    TaskNow.Add(item);
            }

            TaskPast.Reverse();

            ToDoListModelVM init = new()
            {
                Category = CategorySelectList,
                AllTaskNow = TaskNow,
                AllTaskFuture = TaskFuture,
                AllTaskPast = TaskPast,
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
                TimeSpan DueTimeSpan = model.DueDate - DateTime.Now;
                string dataActual = DueTimeSpan.TotalMinutes < 0
                    ? DateTime.Now.ToString()
                    : model.DueDate.ToString();

                CreateTaskModel newTask = new()
                {
                    Task = model.MyTask,
                    DataTime = dataActual,
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
        public IActionResult SetIsCompleted(int id)
        {
            _Service.IsCompleted(id, true, DateTime.Now.ToString());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetServise(int id)
        {
            StoreXML.SetStorage(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetUnCompleted(int id)
        {
            _Service.IsCompleted(id, false, DateTime.Now.ToString());
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int id)
        {
            _Service.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}