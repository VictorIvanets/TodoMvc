
namespace ToDoList.UseDB
{
    public static class SqlQuery
    {
        public readonly static Func<string> getAll = () =>
            "SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.id";

        public readonly static Func< string, string, int, string> addTask = ( string task, string date, int categoryId) =>
            $"INSERT INTO [todolist] ( Task, DueDate, CategoryId ) VALUES ('{task}', '{date}', '{categoryId}')";

        public readonly static Func<int, string> deleteById = (int id) =>
            $"DELETE FROM [todolist] WHERE id = '{id}'";

        public readonly static Func<int, string> getOneById = (int id) =>
            $"SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.id WHERE todolist.id = {id}";

        public readonly static Func<int, string, string, int, string> updateById = (int id, string task, string date, int categoryId) =>
            $"UPDATE [todolist] SET Task = '{task}', DueDate = '{date}', CategoryId = '{categoryId}' WHERE todolist.id = {id}";



        public readonly static Func<int, string> deleteCategory = (int id) =>
            $"DELETE FROM [category] WHERE id = '{id}'";

        public readonly static Func<string, string> addCategory = (string CategoryName) =>
           $"INSERT INTO [category] (CategoryName) VALUES ('{CategoryName}')";

        public readonly static Func<string> getAllCategory = () =>
          "select * from category";


        public readonly static Func<string> ADDTABLE_CAREGORY = () =>
         "CREATE TABLE category (id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, CategoryName VARCHAR(255) NOT NULL)";

        public readonly static Func<string> ADDTABLE_TODOLIST = () =>
         "CREATE TABLE todolist (id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, Task VARCHAR(255) NOT NULL, DueDate VARCHAR(255), CategoryId INT, FOREIGN KEY (CategoryId) REFERENCES category(id))";
    }
}
