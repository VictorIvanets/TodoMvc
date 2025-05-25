using ToDoList.UseDB;
using Microsoft.AspNetCore.Cors;
using ToDoList.Models;


namespace ToDoList.Schems
{
    public class MutationDataGQL
    {


        private readonly SQLService _Services = new();
        [EnableCors("AllowMyOrigin")]

        public async Task<bool> AddTask(CreateTaskModel newTask)
        {
            if (newTask.Task == null || newTask.DataTime == null || newTask.CategoryId == 0)
                throw new GraphQLException(message: "invalid input");
            try
            {
                return await _Services.AddTask(newTask);
            }
            catch (Exception e)
            {
                throw new GraphQLException(message: e.Message);
            }
        }

        public async Task<bool> UpdateTask(int id, CreateTaskModel newTask)
        {
            if (id == 0 || newTask.Task == null || newTask.DataTime == null || newTask.CategoryId == 0)
                throw new GraphQLException(message: "invalid input");
            try
            {
                return await _Services.UpdateTask(id, newTask);
            }
            catch (Exception e)
            {
                throw new GraphQLException(message: e.Message);
            }
        }

        public async Task<bool> SetIsCompleted(int id, byte IsCompleted)
        {
            if (id == 0)
                throw new GraphQLException(message: "invalid input");
            bool forIsCompleted = IsCompleted == 0 ? false : true;
            try
            {
                return await _Services.IsCompleted(id, forIsCompleted, DateTime.Now.ToString());
            }
            catch (Exception e)
            {
                throw new GraphQLException(message: e.Message);
            }
        }

        public async Task<bool> DeleteTask([ID] int id)
        {
            return await _Services.DeleteTask(id);
        }
    }
}
