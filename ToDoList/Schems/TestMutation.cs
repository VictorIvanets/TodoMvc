using GraphQL;
using GraphQL.Types;
using System.Security.Cryptography;

namespace ToDoList.Schems
{
 
    public class TestMutation : ObjectGraphType
    {
        public TestMutation(StarWarsData data)
        {
            Field<HumanType>("createHuman")
              .Argument<NonNullGraphType<HumanInputType>>("human")
              .Resolve(context =>
              {
                  var human = context.GetArgument<Human>("human");
                  return data.AddHuman(human);
              });
        }
    }

    public class HumanType : ObjectGraphType<Human>
    {
        public HumanType()
        {
            Name = "Human";
            Description = "Star Wars universe.";
            Field(d => d.Name, nullable: true).Description("The name of the droid.");
        }
    }

    public class HumanInputType : InputObjectGraphType
    {
        public HumanInputType()
        {
            Name = "HumanInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<StringGraphType>("homePlanet");
        }
    }
    public class StarWarsData
    {
        private List<Human> _humans = new List<Human>();

        public Human AddHuman(Human human)
        {
            human.Id = Guid.NewGuid().ToString();
            _humans.Add(human);
            return human;
        }
    }

    public class Human
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HomePlanet { get; set; }
    }
}
