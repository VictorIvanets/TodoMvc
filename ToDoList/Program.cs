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

builder.Services
    .AddGraphQLServer()
    .AddQueryType <QueryDataGQL>()
    .AddMutationType<MutationDataGQL>();

var app = builder.Build();

app.UseCors("CorsPolicy");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.MapGraphQL("/graphql");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Submit}/{action=Index}/{id?}");
app.Run();
