using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TodoService.Models;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("Todos"));

// Open API services.
// NOTE: .NET 7 enhance the support for fluent OpenAPI configuration.
// However, for .NET 6 we have to configure it manually like shown here.
services.AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new()
        {
            Title = builder.Environment.ApplicationName,
            Version = "v1"
        });

        // Add other OpenAPI features here.
    });


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // Open API application.
    app.UseSwagger()
        .UseSwaggerUI();
}

// Endpoint definition.
app.MapGet("/todo", async (TodoContext db) =>
{
    return await db.TodoItems.ToListAsync();
})
.WithName("FindTodoItems")
.Produces<List<TodoItem>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapGet("/todo/{id}", async (long id, TodoContext db) =>
{
    return await db.TodoItems.FindAsync(id)
        is TodoItem item ? Results.Ok(item) : Results.NotFound();
})
.WithName("GetTodoItem")
.Produces<TodoItem>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/todo", async (TodoItem item, TodoContext db) =>
{
    item.Status = TodoItemStatus.New;
    db.TodoItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/todo/{item.Id}", item);
})
.WithName("CreateTodoItem")
.Produces<TodoItem>(StatusCodes.Status201Created);

app.MapPut("/todo/{id}", async (long id, TodoItem inbound, TodoContext db) =>
{
    var item = await db.TodoItems.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound();
    }

    item.Description = inbound.Description;
    item.DueDate = inbound.DueDate;
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("UpdateTodoItem")
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status204NoContent);


app.MapDelete("/todo/{id}", async (long id, TodoContext db) =>
{
    if (await db.TodoItems.FindAsync(id) is TodoItem item)
    {
        db.TodoItems.Remove(item);
        await db.SaveChangesAsync();
        return Results.Ok(item);
    }
    // HTTP DELETE is suppose to be idempotent. No change in state
    // occurred, so there is no reason to error. So, we return No Content here.
    return Results.NoContent();
})
.WithName("DeleteTodoItem")
.Produces<TodoItem>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status204NoContent);

app.Run();




