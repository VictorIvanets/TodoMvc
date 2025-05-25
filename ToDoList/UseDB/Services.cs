//using ToDoList.Models;

//namespace ToDoList.UseDB
//{
//    public class Services
//    {
//        public async Task<bool> AddTask(CreateTaskModel newTask)
//        {
//            try
//            {
//                return await SQLService.AddTaskSQL(newTask);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        public async Task<bool> UpdateTask(int id, CreateTaskModel newTask)
//        {
//            try
//            {
//                return await SQLService.UpdateByIdSQL(id, newTask);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }


//        public async Task<List<TaskModel>> AllTask()
//        {
//            try
//            {
//                return await SQLService.GetAllSQL();
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        public async Task<List<CategoryModel>> AllCategory()
//        {
//            try
//            {
//                return await SQLService.GetAllCategorySQL();
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }

//        }
//        public async Task<TaskModel> GetOne(int id)
//        {
//            try
//            {
//                return await SQLService.GetOneByIdSQL(id);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        public async Task<bool> SetIsCompleted(int id, bool IsCompleted)
//        {
//            try
//            {
//                return await SQLService.SetIsCompletedByIdSQL(id, IsCompleted, DateTime.Now.ToString());
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }

//        public async Task<bool> DeleteTask(int id)
//        {
//            try
//            {
//                return await SQLService.DeleteByIdSQL(id);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//        }
//    }
//}

////public async Task ADDTABEL()
////{
////    var res = await GetDataFromDB.GetDataList(SqlQuery.ADDTABLE_CAREGORY());
////    var res2 = await GetDataFromDB.GetDataList(SqlQuery.ADDTABLE_TODOLIST());
////}
///




//    public static string DeleteCategory(int id) =>
//$"DELETE FROM [category] WHERE id = '{id}'";

//     public static string AddCategory(string CategoryName) =>
//$"INSERT INTO [category] (CategoryName) VALUES ('{CategoryName}')";

//public readonly static Func<string> ADDTABLE_CAREGORY = () =>
// "CREATE TABLE category (id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, CategoryName VARCHAR(255) NOT NULL)";

//public readonly static Func<string> ADDTABLE_TODOLIST = () =>
// "CREATE TABLE todolist (id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, Task VARCHAR(5000) NOT NULL, DueDate VARCHAR(255), CategoryId INT, IsCompleted BIT, FOREIGN KEY (CategoryId) REFERENCES category(id))";


//    XDocument xmlDoc = new XDocument(
//    new XDeclaration("1.0", "utf-8", null),
//    new XElement("Root",
//        new XElement("MyTask",
//            new XAttribute("Id", "1"),
//            new XElement("Task", task.Task),
//            new XElement("DataTime", task.DataTime),
//            new XElement("CategoryId", task.CategoryId),
//            new XElement("IsCompleted", (byte)(task.IsCompleted ? 1 : 0))
//        )
//    )
//);