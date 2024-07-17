using Emp.Data.Context;
using Emp.Data.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

builder.Services.DbContextExtension(builder.Configuration);
builder.Services.RepositoryExtension(builder.Configuration);
builder.Services.IUnitOfWorkExtension();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

