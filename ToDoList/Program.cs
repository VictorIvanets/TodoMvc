using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using ToDoList.GraphQLTypes;
using ToDoList.Schems;
using ToDoList.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IToDoServiceFactory, ToDoServiceFactory>()
                .AddScoped<TodoQuery>()
                .AddScoped<ToDoMutation>()
                .AddScoped<GraphQL.Types.ISchema, ToDoSchema>();

builder.Services
    .AddGraphQL(builder => builder
        .AddSystemTextJson()
        .AddSchema<ToDoSchema>()
        .AddGraphTypes()
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapGraphQL("/graphql");
app.MapGraphQLPlayground("/ui/playground");

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}/{Id?}");
app.Run();
