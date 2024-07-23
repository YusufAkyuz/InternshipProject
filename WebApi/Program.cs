using Emp.Data.Context;
using Emp.Data.Extensions;
using Emp.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

builder.Services.DbContextExtension(builder.Configuration);
builder.Services.RepositoryExtension(builder.Configuration);
builder.Services.UnitOfWorkExtension();
builder.Services.UserServiceExtension();

Log.Logger = new LoggerConfiguration().WriteTo.Seq(serverUrl : "http://localhost:5341", apiKey:"GUev3iQYqovx4WUfqM3t").CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();

