using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.GraphQLTypes
{

    public class InputUpdateTaskTypes : InputObjectGraphType<CreateTaskModel>
    {
        public InputUpdateTaskTypes()
        {
            Name = "UpdateTask";
            Field(x => x.Id);
            Field(x => x.Task);
            Field(x => x.DateTime);
            Field(x => x.CategoryId);
        }
    }
}
