using GraphQL;
using GraphQL.Types;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Schems
{
    public class ToDoMutation : ObjectGraphType
    {
        public ToDoMutation()
        {
            Field<bool>("addTask")
                .Argument<NonNullGraphType<InputTaskTypes>>("addTask")
                .Resolve(context =>
                {
                    try
                    {
                        var task = context.GetArgument<CreateTaskModel>("addTask");

                        CreateTaskModel newTask = new CreateTaskModel
                        {
                            Task = task.Task,
                            DataTime = task.DataTime,
                            CategoryId = task.CategoryId,
                            IsCompleted = false,
                        };
                        return GetDB.Service().AddTask(newTask);
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
                       var getid = context.GetArgument<bool>("isSql");
                       if (getid) StoreXML.SetStorage(1);                   
                       else StoreXML.SetStorage(2);                     
                       return StoreXML.GetStorage() ? "SQL" : "XML";
                   }
                   catch (Exception ex)
                   {
                       throw new ExecutionError(ex.Message);
                   }
               });

            Field<bool>("updateTask")
             .Argument<NonNullGraphType<InputUpdateTaskTypes>>("task")
             .Resolve(context =>
             {
                 try
                 {
                     var task = context.GetArgument<CreateTaskModel>("task");

                     CreateTaskModel newTask = new CreateTaskModel
                     {
                         Task = task.Task,
                         DataTime = task.DataTime,
                         CategoryId = task.CategoryId,
                         IsCompleted = false,
                     };
                     return GetDB.Service().UpdateTask(task.Id, newTask);
                 }
                 catch (Exception ex)
                 {
                     throw new ExecutionError(ex.Message);
                 }
             });

            Field<bool>("DeleteTask")
              .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
              .Resolve(context =>
              {
                  var id = context.GetArgument<int?>("id");
                  if (id.HasValue)
                  {
                      int Id = id.Value;
                      try
                      {
                          return GetDB.Service().DeleteTask(Id);
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
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
              .Resolve(context =>
              {
                  var id = context.GetArgument<int?>("id");
                  if (id.HasValue)
                  {
                      int Id = id.Value;
                      try
                      {
                          return GetDB.Service().IsCompleted(Id, true, DateTime.Now.ToString());
                      }
                      catch (Exception ex)
                      {
                          throw new ExecutionError(ex.Message);
                      }
                  }
                  throw new ExecutionError("error id");
              });

            Field<bool>("SetUnCompleted")
              .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
              .Resolve(context =>
              {
                  var id = context.GetArgument<int?>("id");
                  if (id.HasValue)
                  {
                      int Id = id.Value;
                      try
                      {
                          return GetDB.Service().IsCompleted(Id, false, DateTime.Now.ToString());
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
