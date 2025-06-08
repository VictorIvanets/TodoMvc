namespace ToDoList.Schems
{
    public class ToDoSchema : GraphQL.Types.Schema
    {
        public ToDoSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<TodoQuery>();
            Mutation = provider.GetRequiredService<ToDoMutation>();
        }
    }
}
