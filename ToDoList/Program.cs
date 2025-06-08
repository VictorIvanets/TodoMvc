using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using ToDoList.GraphQLTypes;
using ToDoList.Schems;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});

//builder.Services
//    .AddGraphQLServer()
//    .AddQueryType <QueryDataGQL>()
//    .AddMutationType<MutationDataGQL>();



builder.Services.AddSingleton<TodoQuery>();
builder.Services.AddSingleton<ToDoMutation>();

builder.Services.AddSingleton<GraphQL.Types.ISchema, ToDoSchema>(services => new ToDoSchema(new SelfActivatingServiceProvider(services)));

builder.Services
    .AddGraphQL(builder => builder
        .AddSystemTextJson()
        .AddSchema<ToDoSchema>()
        .AddGraphTypes()
    );





var app = builder.Build();

app.UseCors("CorsPolicy");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapGraphQL("/graphql");
app.MapGraphQLPlayground("/ui/playground");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}/{Id?}");
app.Run();
