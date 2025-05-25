using Microsoft.AspNetCore.Cors;
using ToDoList.Models;
using ToDoList.UseDB;

namespace ToDoList.Schems
{
    public class QueryDataGQL
    {

        private readonly SQLService _Services = new();
        [EnableCors("AllowMyOrigin")]
        public async Task<List<TaskModel>> AllTask()
        {
            return await _Services.AllTask();
        }

        public async Task<List<CategoryModel>> AllCategory()
        {
            return await _Services.GetAllCategory();
        }

        public async Task<TaskModel> GetOne([ID] int id)
        {
            return await _Services.GetOne(id);
        }
    }
}
