using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.Schems
{
    //public class Query : ObjectGraphType
    //{
    //    public Query()
    //    {
    //        Field<StringGraphType>("some").Resolve(context => "i work");
    //    }
    //}

    public class Query

    {
        public static testInpitModel AnyQuery()
        {
            return new testInpitModel
            {
                name = "GraphQl",
                description = "work only query"
            };
        }
    }
}
