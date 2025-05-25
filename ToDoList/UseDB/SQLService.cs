
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.UseDB
{
     public class SQLService
    {
        private readonly  DataBase _db = new();

        public  async Task<List<TaskModel>> AllTask()
        {
            try
            {
                string commandText = "SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.id";
                List<TaskModel> taskList = new List<TaskModel>();
                await _db.openConnection();
                await using SqlCommand command = new SqlCommand(commandText, _db.getConnection());
                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    TaskModel oneTask = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        oneTask.id = Int32.Parse(reader.GetValue(0).ToString());
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
                await _db.closeConnection();
            }
        }


        public async Task<bool> AddTask(CreateTaskModel task)
        {
            try
            {
                string commandText = "INSERT INTO [todolist] ( Task, DueDate, CategoryId, IsCompleted ) VALUES (@newTask, @date, @categoryId, @IsCompleted)";
                await using SqlCommand command = new SqlCommand(commandText, _db.getConnection());
                command.Parameters.AddWithValue("@newTask", task.Task);
                command.Parameters.AddWithValue("@date", task.DataTime);
                command.Parameters.AddWithValue("@categoryId", task.CategoryId);
                command.Parameters.AddWithValue("@IsCompleted", (byte)(task.IsCompleted ? 1 : 0));
                await _db.openConnection();
                await command.ExecuteNonQueryAsync();
                return true;

            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                await _db.closeConnection();
            }
        }


        public async Task<bool> UpdateTask(int id, CreateTaskModel task)
        {
            try
            {
                string commandText = "UPDATE [todolist] SET Task = @newTask, DueDate = @date, CategoryId = @categoryId, IsCompleted = @IsCompleted WHERE todolist.id = @id";
                await using SqlCommand command = new SqlCommand(commandText, _db.getConnection());
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@newTask", task.Task);
                command.Parameters.AddWithValue("@date", task.DataTime);
                command.Parameters.AddWithValue("@categoryId", task.CategoryId);
                command.Parameters.AddWithValue("@IsCompleted", (byte)(task.IsCompleted ? 1 : 0));
                await _db.openConnection();
                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                await _db.closeConnection();
            }
        }



        public async Task<TaskModel> GetOne(int id)
        {
            try
            {
                string commandText = $"SELECT * FROM todolist INNER JOIN category ON todolist.CategoryId = category.id WHERE todolist.id = @id";
                TaskModel OneTask = new TaskModel();
                await _db.openConnection();
                await using SqlCommand command = new SqlCommand(commandText, _db.getConnection());
                command.Parameters.AddWithValue("@id", id);
                await using SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    OneTask.id = Int32.Parse(reader.GetValue(0).ToString());
                    OneTask.MyTask = reader.GetValue(1).ToString();
                    OneTask.DueDate = DateTime.Parse(reader.GetValue(2).ToString());
                    OneTask.DueTimeSpan = DateTime.Parse(reader.GetValue(2).ToString()) - DateTime.Now;
                    OneTask.CategoryId = Int32.Parse(reader.GetValue(3).ToString());
                    OneTask.IsCompleted = reader.GetValue(4).ToString() == "True" ? true : false;
                    OneTask.CategoryName = reader.GetValue(6).ToString();
                }
                return OneTask;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                await _db.closeConnection();
            }
        }

        public async Task<bool> DeleteTask(int id)
        {
            try
            {
                string commandText = "DELETE FROM [todolist] WHERE id = @id";
                await using SqlCommand command = new SqlCommand(commandText, _db.getConnection());
                command.Parameters.AddWithValue("@id", id);
                await _db.openConnection();
                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                await _db.closeConnection();
            }
        }

        public async Task<bool> IsCompleted(int id, bool isCompleted, string date)
        {
            try
            {
                byte forIsCompleted = (byte)(isCompleted ? 1 : 0);
                string commandText = "UPDATE [todolist] SET IsCompleted = @forIsCompleted, DueDate = @date WHERE todolist.id = @id";
                await using SqlCommand command = new SqlCommand(commandText, _db.getConnection());
                command.Parameters.AddWithValue("@forIsCompleted", forIsCompleted);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@id", id);
                await _db.openConnection();
                await command.ExecuteNonQueryAsync();
                return true;

            }
            catch (Exception e)
            {
                var testError = e.Message;
                throw new Exception(e.Message);
            }
            finally
            {
                await _db.closeConnection();
            }
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            try
            {
                string commandText = "select * from category";
                List<CategoryModel> categoryList = new List<CategoryModel>();
                await _db.openConnection();
                await using SqlCommand command = new SqlCommand(commandText, _db.getConnection());
                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    CategoryModel oneCategory = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        oneCategory.id = Int32.Parse(reader.GetValue(0).ToString());
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
                await _db.closeConnection();
            }
        }
    }
}
