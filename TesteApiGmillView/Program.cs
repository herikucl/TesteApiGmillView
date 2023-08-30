using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using TesteApiGmillView.Context;
using TesteApiGmillView.Maps;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Api
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<CompanyContext>(options =>
                            options.UseNpgsql("Host=127.0.0.1;Port=5432;Pooling=true;Database=TesteApiGmil;User Id=postgres;Password=1459;"));
builder.Services.AddAutoMapper(typeof(RequestToModel));
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger(c =>
{
    c.RouteTemplate = "Home/swagger/{documentname}/swagger.json";
});


app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/Home/swagger/v1/swagger.json", "TesteApiGmillView");
    c.RoutePrefix = "Home/swagger";
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Companies}/{id?}");


app.Run();
