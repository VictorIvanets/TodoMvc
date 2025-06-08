using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQLTypes;
using ToDoList.Services;

namespace ToDoList.Schems
{
    public class TodoQuery : ObjectGraphType
    {
        public TodoQuery()
        {
            Field<ListGraphType<ToDoType>>("alltask")
              .Resolve(context => GetDB.Service().AllTask());

            Field<ListGraphType<CategoryType>>("allCategory")
              .Resolve(context => GetDB.Service().GetAllCategory());

            Field<ToDoType>("oneTask")
              .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
              .Resolve(context =>
                {
                     var id = context.GetArgument<int?>("id");
                     if (id.HasValue)
                     {
                         int Id = id.Value;                  
                         try {
                             return GetDB.Service().GetOne(Id);
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
