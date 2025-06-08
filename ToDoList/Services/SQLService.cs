using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class SQLService : Service
    {
        private readonly DataBase _db = new();

        public override List<TaskModel> AllTask()
        {
            try
            {
                string sqlString = "SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.Id";
                List<TaskModel> taskList = [];
                _db.OpenConnection();
                using SqlCommand command = new SqlCommand(sqlString, _db.GetConnection());
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(reader.FieldCount == 0)
                        throw new Exception("No data found in the todolist table.");
                    TaskModel oneTask = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        oneTask.Id = Int32.Parse(reader.GetValue(0).ToString());
                        oneTask.MyTask = reader.GetValue(1).ToString();
                        oneTask.DueDate = DateTime.Parse(reader.GetValue(2).ToString());
                        oneTask.DueTimeSpan = DateTime.Parse(reader.GetValue(2).ToString()) - DateTime.Now;
                        oneTask.CategoryId = Int32.Parse(reader.GetValue(3).ToString());
                        oneTask.IsCompleted = reader.GetValue(4).ToString() == "True" ? true : false;
                        oneTask.CategoryName = reader.GetValue(6).ToString();
                    }
                    taskList.Add(oneTask);
                }
                return [.. taskList.OrderBy(o => o.DueTimeSpan.TotalMinutes)];
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
               _db.CloseConnection();
            }
        }


        public override bool AddTask(CreateTaskModel task)
        {
            try
            {
                string sqlString = "INSERT INTO [todolist] ( Task, DueDate, CategoryId, IsCompleted ) VALUES (@newTask, @date, @categoryId, @IsCompleted)";
                using SqlCommand command = new(sqlString, _db.GetConnection());
                command.Parameters.AddWithValue("@newTask", task.Task);
                command.Parameters.AddWithValue("@date", task.DataTime);
                command.Parameters.AddWithValue("@categoryId", task.CategoryId);
                command.Parameters.AddWithValue("@IsCompleted", (byte)(task.IsCompleted ? 1 : 0));
                _db.OpenConnection();
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                _db.CloseConnection();
            }
        }


        public override bool UpdateTask(int id, CreateTaskModel task)
        {
            try
            {
                string sqlString = "UPDATE [todolist] SET Task = @newTask, DueDate = @date, CategoryId = @categoryId, IsCompleted = @IsCompleted WHERE todolist.Id = @Id";
                using SqlCommand command = new(sqlString, _db.GetConnection());
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@newTask", task.Task);
                command.Parameters.AddWithValue("@date", task.DataTime);
                command.Parameters.AddWithValue("@categoryId", task.CategoryId);
                command.Parameters.AddWithValue("@IsCompleted", (byte)(task.IsCompleted ? 1 : 0));
                _db.OpenConnection();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                _db.CloseConnection();
            }
        }

      
        public override TaskModel GetOne(int id)
        {
            try
            {
                string sqlString = $"SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.Id WHERE todolist.Id = @Id";
                TaskModel oneTask = new();
                _db.OpenConnection();
                using SqlCommand command = new(sqlString, _db.GetConnection());
                command.Parameters.AddWithValue("@Id", id);
                using SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                    throw new Exception($"No task found with Id: {id}");
                while (reader.Read())
                {
                    oneTask.Id = Int32.Parse(reader.GetValue(0).ToString());
                    oneTask.MyTask = reader.GetValue(1).ToString();
                    oneTask.DueDate = DateTime.Parse(reader.GetValue(2).ToString());
                    oneTask.DueTimeSpan = DateTime.Parse(reader.GetValue(2).ToString()) - DateTime.Now;
                    oneTask.CategoryId = Int32.Parse(reader.GetValue(3).ToString());
                    oneTask.IsCompleted = reader.GetValue(4).ToString() == "True" ? true : false;
                    oneTask.CategoryName = reader.GetValue(6).ToString();
                }
                return oneTask;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                _db.CloseConnection();
            }
        }

        public override bool DeleteTask(int id)
        {
            try
            {
                string sqlString = "DELETE FROM [todolist] WHERE Id = @Id";
                using SqlCommand command = new SqlCommand(sqlString, _db.GetConnection());
                command.Parameters.AddWithValue("@Id", id);
                _db.OpenConnection();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                _db.CloseConnection();
            }
        }

        public override bool IsCompleted(int id, bool isCompleted, string date)
        {
            try
            {
                string sqlString = "UPDATE [todolist] SET IsCompleted = @forIsCompleted, DueDate = @date WHERE todolist.Id = @Id";
                using SqlCommand command = new(sqlString, _db.GetConnection());
                command.Parameters.AddWithValue("@forIsCompleted", (byte)(isCompleted ? 1 : 0));
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@Id", id);
                _db.OpenConnection();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                _db.CloseConnection();
            }
        }

        public override List<CategoryModel> GetAllCategory()
        {
            try
            {
                string sqlString = "select * from category";
                List<CategoryModel> categoryList = [];
                _db.OpenConnection();
                using SqlCommand command = new(sqlString, _db.GetConnection());
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CategoryModel oneCategory = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        oneCategory.Id = Int32.Parse(reader.GetValue(0).ToString());
                        oneCategory.CategoryName = reader.GetValue(1).ToString();
                    }
                    categoryList.Add(oneCategory);
                }
                return categoryList;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                _db.CloseConnection();
            }
        }
    }
}
