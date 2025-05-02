
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.UseDB
{
    public static class SqlQuery
    {
        public readonly static Func<string> getAll = () =>
            "SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.id";

        public readonly static Func<string, string, int, bool, string> addTask = (string task, string date, int categoryId, bool isCompleted) =>
        {
            string newTask = task.Replace("'", "`");
            byte forIsCompleted = (byte)(isCompleted ? 1 : 0);

            return $"INSERT INTO [todolist] ( Task, DueDate, CategoryId, IsCompleted ) VALUES ('{newTask}', '{date}', '{categoryId}', '{forIsCompleted}')";
        };
           
        public readonly static Func<int, string> deleteById = (int id) =>
            $"DELETE FROM [todolist] WHERE id = '{id}'";

        public readonly static Func<int, string> getOneById = (int id) =>
            $"SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.id WHERE todolist.id = {id}";

        public readonly static Func<int, string, string, int, string> updateById = (int id, string task, string date, int categoryId) =>
        {
            string newTask = task.Replace("'", "`");
            return $"UPDATE [todolist] SET Task = '{newTask}', DueDate = '{date}', CategoryId = '{categoryId}' WHERE todolist.id = {id}"; 
        };

        public readonly static Func<int, bool, string, string> SetIsCompletedById = (int id, bool isCompleted, string date) =>
        {
            byte forIsCompleted = (byte)(isCompleted ? 1 : 0);
            return $"UPDATE [todolist] SET IsCompleted = '{forIsCompleted}', DueDate = '{date}' WHERE todolist.id = {id}";
        };


        public readonly static Func<int, string> deleteCategory = (int id) =>
            $"DELETE FROM [category] WHERE id = '{id}'";

        public readonly static Func<string, string> addCategory = (string CategoryName) =>
           $"INSERT INTO [category] (CategoryName) VALUES ('{CategoryName}')";

        public readonly static Func<string> getAllCategory = () =>
          "select * from category";


        public readonly static Func<string> ADDTABLE_CAREGORY = () =>
         "CREATE TABLE category (id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, CategoryName VARCHAR(255) NOT NULL)";

        public readonly static Func<string> ADDTABLE_TODOLIST = () =>
         "CREATE TABLE todolist (id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, Task VARCHAR(5000) NOT NULL, DueDate VARCHAR(255), CategoryId INT, IsCompleted BIT, FOREIGN KEY (CategoryId) REFERENCES category(id))";
    }
}
