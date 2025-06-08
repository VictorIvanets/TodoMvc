using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.Schems
{
    public class InputTaskTypes : InputObjectGraphType<CreateTaskModel>
    {
        public InputTaskTypes()
        {
            Name = "AddTask";
            Field(x => x.Task);
            Field(x => x.DataTime);
            Field(x => x.CategoryId);
        }
    }
}
