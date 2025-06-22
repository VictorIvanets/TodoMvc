using GraphQL;
using GraphQL.Types;
using System.Threading.Tasks;
using ToDoList.GraphQLTypes;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Schems
{
    public class ToDoMutation : ObjectGraphType
    {
        public ToDoMutation(IToDoServiceFactory factory)
        {
            Service _service = factory.GetService();

            Field<ToDoType>("addTask")
                .Argument<NonNullGraphType<InputTaskTypes>>("addTask")
                .Resolve(context =>
                {
                    try
                    {
                        CreateTaskModel task = context.GetArgument<CreateTaskModel>("addTask");

                        CreateTaskModel newTask = new()
                        {
                            Task = task.Task,
                            DateTime = task.DateTime,
                            CategoryId = task.CategoryId,
                            IsCompleted = false,
                        };
                        return _service.GetOne(_service.AddTask(newTask));                   
                    }
                    catch (Exception ex)
                    {
                        throw new ExecutionError(ex.Message);
                    }
                });


            Field<string>("SetStorage")
               .Argument<NonNullGraphType<BooleanGraphType>>("isSql")
               .Resolve(context =>
               {
                   try
                   {
                       bool getid = context.GetArgument<bool>("isSql");
                       if (getid) StoreXML.SetStorage(1);                   
                       else StoreXML.SetStorage(2);                     
                       return StoreXML.GetStorage() ? "SQL" : "XML";
                   }
                   catch (Exception ex)
                   {
                       throw new ExecutionError(ex.Message);
                   }
               });

            Field<ToDoType>("updateTask")
             .Argument<NonNullGraphType<InputUpdateTaskTypes>>("task")
             .Resolve(context =>
             {
                 try
                 {
                     CreateTaskModel task = context.GetArgument<CreateTaskModel>("task");

                     CreateTaskModel newTask = new CreateTaskModel
                     {
                         Task = task.Task,
                         DateTime = task.DateTime,
                         CategoryId = task.CategoryId,
                         IsCompleted = false,
                     };

                     return _service.GetOne(_service.UpdateTask(task.Id, newTask));
                 }
                 catch (Exception ex)
                 {
                     throw new ExecutionError(ex.Message);
                 }
             });

            Field<IdGraphType>("DeleteTask")
              .Arguments(new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ))
              .Resolve(context =>
              {
                  int? id = context.GetArgument<int?>("id");
                  if (id.HasValue)
                  {
                      try
                      {
                          return _service.DeleteTask(id.Value);
                      }
                      catch (Exception ex)
                      {
                          throw new ExecutionError(ex.Message);
                      }
                  }
                  throw new ExecutionError("error id");
              });

            Field<bool>("SetIsCompleted")
              .Arguments(new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }
                ))
              .Resolve(context =>
              {
                  int? id = context.GetArgument<int?>("id");
                  if (id.HasValue)
                  {
                      try
                      {
                          return _service.IsCompleted(id.Value, DateTime.Now.ToString());
                          

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
