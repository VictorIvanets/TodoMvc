using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQLTypes;
using ToDoList.Services;

namespace ToDoList.Schems
{
    public class TodoQuery : ObjectGraphType
    {
        public TodoQuery(IToDoServiceFactory factory)
        {
            var _service = factory.GetService();

            Field<ListGraphType<ToDoType>>("allTask")
              .Resolve(context => _service.AllTask());

            Field<ListGraphType<CategoryType>>("allCategory")
              .Resolve(context => _service.GetAllCategory());

            Field<ToDoType>("oneTask")
              .Arguments(new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ))
              .Resolve(context =>
                {
                     int? id = context.GetArgument<int?>("id");
                     if (id.HasValue)
                     {               
                         try {
                             return _service.GetOne(id.Value);
                         }
                         catch (Exception ex)
                         {
                             throw new ExecutionError(ex.Message);
                         }
                     }
                     throw new ExecutionError("error id");
                });
        }
    }
}
