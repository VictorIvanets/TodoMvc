using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.Schems
{
  
    public class InputUpdateTaskTypes : InputObjectGraphType<CreateTaskModel>
    {
        public InputUpdateTaskTypes()
        {
            Name = "UpdateTask";
            Field(x => x.Id);
            Field(x => x.Task);
            Field(x => x.DataTime);
            Field(x => x.CategoryId);
        }
    }
}
