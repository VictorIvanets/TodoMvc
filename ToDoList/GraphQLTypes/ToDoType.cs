using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.GraphQLTypes
{
    public class ToDoType : ObjectGraphType<TaskModel>
    {
        public ToDoType()
        {
            Field(x => x.Id);
            Field(x => x.MyTask);
            Field(x => x.DueDate);
            Field(x => x.DueTimeSpan);
            Field(x => x.CategoryId);
            Field(x => x.CategoryName);
            Field(x => x.IsCompleted);
        }
    }
}
