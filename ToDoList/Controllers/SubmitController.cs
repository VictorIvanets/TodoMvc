using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.UseDB;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ToDoList.Controllers
{
    public class SubmitController : Controller
    {
        private readonly SQLService _Services = new();
        private readonly XMLService _ServicesX = new();
        private bool useServise = StoreXML.GetStorage();

        public async Task<IActionResult> Index()
        {
            var TEST = useServise;
            var TESTRES = useServise;

            List<TaskModel> getalltask = useServise ? await _Services.AllTask() : _ServicesX.AllTask();
            List<CategoryModel> category =  useServise? await _Services.GetAllCategory() : _ServicesX.GetAllCategory();
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
                if (item.DueTimeSpan.TotalMinutes < 0 || item.IsCompleted)
                    TaskPast.Add(item);
                else if (item.DueTimeSpan.TotalMinutes > 120 && !item.IsCompleted)
                    TaskFuture.Add(item);
                else if(item.DueTimeSpan.TotalMinutes > 0 && item.DueTimeSpan.TotalMinutes < 120 && !item.IsCompleted)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int? id)
        {

            List<TaskModel> getalltask = useServise 
                ? await _Services.AllTask() 
                : _ServicesX.AllTask();
            List<CategoryModel> category = useServise 
                ? await _Services.GetAllCategory() 
                : _ServicesX.GetAllCategory();
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
                else if (item.DueTimeSpan.TotalMinutes > 0 
                    && item.DueTimeSpan.TotalMinutes < 120)

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
                TaskModel oneTaskById = useServise ? await _Services.GetOne((int)id) : _ServicesX.GetOne((int)id);
                init.id = oneTaskById.id;
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
        public async Task<IActionResult> AddTask(SubmitModelVM model)
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
                if (model.id != 0)
                {
                    bool update  = useServise 
                        ? await _Services.UpdateTask(model.id, newTask) 
                        : _ServicesX.UpdateTask(model.id, newTask);
                    return RedirectToAction("Index");
                }
                bool added = useServise 
                    ? await _Services.AddTask(newTask) 
                    : _ServicesX.AddTask(newTask);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SetIsCompleted(int id)
        {
            bool isCompl = useServise 
                ? await _Services.IsCompleted(id, true, DateTime.Now.ToString()) 
                : _ServicesX.IsCompleted(id, true, DateTime.Now.ToString());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetServise(int id)
        {
            StoreXML.SetStorage(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SetUnCompleted(int id)
        {
            bool unCompl = useServise 
                ? await _Services.IsCompleted(id, false, DateTime.Now.ToString()) 
                : _ServicesX.IsCompleted(id, false, DateTime.Now.ToString());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            bool del = useServise 
                ? await _Services.DeleteTask(id) 
                : _ServicesX.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}