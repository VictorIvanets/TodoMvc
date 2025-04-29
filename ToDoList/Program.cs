using GraphQL;
using GraphQL.Server.Ui.GraphiQL;
using ToDoList.Schems;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddGraphQL(b => b
    //.AddAutoSchema<MainSchema>()
    .AddAutoSchema<Query>()  
    .AddSystemTextJson()); 



var app = builder.Build();


app.UseGraphQL("/graphql");      
app.UseGraphQLGraphiQL(
    "/api",           
    new GraphQL.Server.Ui.GraphiQL.GraphiQLOptions
    {
        GraphQLEndPoint = "/graphql", 
        SubscriptionsEndPoint = "/graphql", 
    });



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Submit}/{action=Index}/{id?}");

app.Run();
