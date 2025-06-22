using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.GraphQLTypes
{
    public class InputTaskTypes : InputObjectGraphType<CreateTaskModel>
    {
        public InputTaskTypes()
        {
            Name = "AddTask";
            Field(x => x.Task);
            Field(x => x.DateTime);
            Field(x => x.CategoryId);
        }
    }
}
