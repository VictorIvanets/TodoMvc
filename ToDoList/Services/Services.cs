using ToDoList.Models;

namespace ToDoList.Services
{
    public abstract class Service
    {
        public abstract bool AddTask(CreateTaskModel newTask);

        public abstract bool UpdateTask(int id, CreateTaskModel newTask);

        public abstract List<TaskModel> AllTask();

        public abstract List<CategoryModel> GetAllCategory();

        public abstract TaskModel GetOne(int id);

        public abstract bool IsCompleted(int id, bool isCompleted, string date);

        public abstract bool DeleteTask(int id);
     
    }
}


























////public async Task ADDTABEL()
////{
////    var res = await GetDataFromDB.GetDataList(SqlQuery.ADDTABLE_CAREGORY());
////    var res2 = await GetDataFromDB.GetDataList(SqlQuery.ADDTABLE_TODOLIST());
////}
///

//    public static string DeleteCategory(int Id) =>
//$"DELETE FROM [category] WHERE Id = '{Id}'";

//public static string AddCategory(string CategoryName) =>
//$"INSERT INTO [category] (CategoryName) VALUES ('{CategoryName}')";

//public readonly static Func<string> ADDTABLE_CAREGORY = () =>
// "CREATE TABLE category (Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, CategoryName VARCHAR(255) NOT NULL)";

//public readonly static Func<string> ADDTABLE_TODOLIST = () =>
// "CREATE TABLE todolist (Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, Task VARCHAR(5000) NOT NULL, DueDate VARCHAR(255), CategoryId INT, isCompleted BIT, FOREIGN KEY (CategoryId) REFERENCES category(Id))";


//XDocument xmlDoc = new XDocument(
//new XDeclaration("1.0", "utf-8", null),
//new XElement("Root",
//    new XElement("MyTask",
//        new XAttribute("Id", "1"),
//        new XElement("Task", task.Task),
//        new XElement("DataTime", task.DataTime),
//        new XElement("CategoryId", task.CategoryId),
//        new XElement("isCompleted", (byte)(task.isCompleted ? 1 : 0))
//    )
//)
//);