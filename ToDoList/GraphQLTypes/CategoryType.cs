using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.GraphQLTypes
{
    public class CategoryType : ObjectGraphType<CategoryModel>
    {
        public CategoryType()
        {
            Field(x => x.Id);
            Field(x => x.CategoryName);
        }
    }
}
