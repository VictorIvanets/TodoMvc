using GraphQL.Types;

namespace ToDoList.Schems
{
    public class MainSchema : Schema
    {
        public MainSchema(IServiceProvider provider) : base(provider)
        {
            //Query = provider.GetRequiredService<Query>();
        }
    }

}



