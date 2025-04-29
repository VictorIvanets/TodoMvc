using GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Schems
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery()
        {
            Field<DroidType>("hero")
              .Resolve(context => new Droid { Id = "1", Name = "R2-D2" });
        }

        //public static string Hero() => "Luke Skywalker";
        //public string Droid() => "R2-D2";
       
    }
}

public class DroidType : ObjectGraphType<Droid>
{
    public DroidType()
    {
        Name = "Droid";
        Description = "A mechanical creature in the Star Wars universe.";
        Field(d => d.Name, nullable: true).Description("The name of the droid.");
    }
}

public class Droid
{
    public string Id { get; set; }
    public string Name { get; set; }
}
