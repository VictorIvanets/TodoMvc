using ToDoList.Models;

namespace ToDoList.UseDB
{
    public class Services
    {

        public async Task AddTask(string task, string datatime, int categoryId, bool IsCompleted)
        {
            List<List<string>> data = await GetDataFromDB.getDataList(SqlQuery.addTask( task, datatime, categoryId, IsCompleted));
        }

        public async Task<List<TaskModel>> AllTask()
        {
            List<List<string>> data = await GetDataFromDB.getDataList(SqlQuery.getAll());

            List<TaskModel> alltask = new();

            foreach (var item in data)
            {
                alltask.Add(new TaskModel
                {
                    id = Int32.Parse(item[0]),
                    MyTask = item[1],
                    DueDate = DateTime.Parse(item[2]),
                    DueTimeSpan = DateTime.Parse(item[2]) - DateTime.Now,
                    CategoryId = Int32.Parse(item[3]),
                    IsCompleted = item[4] == "True" ? true : false,
                    CategoryName = item[6],
                });
            }
            return [.. alltask.OrderBy(o => o.DueTimeSpan.TotalMinutes)];
          }

        public async Task<List<CategoryModel>> AllCategory()
        {

            List<List<string>> data = await GetDataFromDB.getDataList(SqlQuery.getAllCategory());

            List<CategoryModel> allcategory = new();

            foreach (var item in data)
            {
                allcategory.Add(new CategoryModel
                {
                    id = Int32.Parse(item[0]),
                    CategoryName = item[1]
                });
            }
            return allcategory;
        }
        public async Task<TaskModel> GetOne(int id)
        {
            List<List<string>> data = await GetDataFromDB.getDataList(SqlQuery.getOneById(id));

            TaskModel onetask = new TaskModel
            {
                id = Int32.Parse(data[0][0]),
                MyTask = data[0][1],
                DueDate = DateTime.Parse(data[0][2]),
                DueTimeSpan = DateTime.Parse(data[0][2]) - DateTime.Now,
                CategoryId = Int32.Parse(data[0][3]),
                CategoryName = data[0][6],
            };
            return onetask;
        }
        public async Task UpdateTask(int id, string task, string datatime, int categoryId)
        {
            await GetDataFromDB.getDataList(SqlQuery.updateById(id, task, datatime, categoryId));
        }

        public async Task SetIsCompleted(int id, bool IsCompleted)
        {
            await GetDataFromDB.getDataList(SqlQuery.SetIsCompletedById(id, IsCompleted, DateTime.Now.ToString()));
        }

        public async Task ADDTABEL()
        {
            var res = await GetDataFromDB.getDataList(SqlQuery.ADDTABLE_CAREGORY());
            var res2 = await GetDataFromDB.getDataList(SqlQuery.ADDTABLE_TODOLIST());
        }

        public async Task DeleteTask(int id)
        {
            await GetDataFromDB.getDataList(SqlQuery.deleteById(id));
        }
    }
}
