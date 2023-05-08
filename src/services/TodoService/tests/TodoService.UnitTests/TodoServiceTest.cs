using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TodoService.Models;
using System;
using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace TodoService
{
    public class TodoServiceTest
    {
        public TodoServiceTest()
        {
        }


        private async Task SetupDatabase(TodoApplication application, Action<TodoContext> configure = null)
        {
            using (var scope = application.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
                if (configure != null)
                {
                    configure(db);
                }
                await db.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task FindAll_ItemsArePresent()
        {

            await using var application = new TodoApplication();
            var client = application.CreateClient();
            await SetupDatabase(application, async db =>
            {
                await db.AddAsync(new TodoItem()
                {
                    Description = "Test Item 1",
                    Status = TodoItemStatus.New
                });
            });
            var result = await client.GetFromJsonAsync<List<TodoItem>>("/todo");
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task FindAll_ItemsAreNotPresent()
        {
            await using var application = new TodoApplication();
            var client = application.CreateClient();
            var result = await client.GetFromJsonAsync<List<TodoItem>>("/todo");
            Assert.Empty(result);
        }

        [Fact]
        public async Task Get_ItemIsNotPresent_ReturnsNotFound()
        {
            await using var application = new TodoApplication();
            var client = application.CreateClient();
            var result = await client.GetAsync("/todo/1");
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

        }

        [Fact]
        public async Task Get_ItemIsPresent_ReturnsOK()
        {
            await using var application = new TodoApplication();
            var client = application.CreateClient();
            await SetupDatabase(application, async db =>
            {
                await db.AddAsync(new TodoItem()
                {
                    Description = "Test Item 1",
                    Status = TodoItemStatus.New
                });
            });
            var result = await client.GetAsync("/todo/1");
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        }

        [Fact]
        public async Task Create_NewItem()
        {

            var newItem = new TodoItem()
            {
                Description = "Test Item #1",
                DueDate = DateTimeOffset.Parse("2023-01-01 10:00:00.000Z")
            };

            await using var application = new TodoApplication();
            var client = application.CreateClient();
            var response = await client.PostAsJsonAsync("/todo", newItem);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var items = await client.GetFromJsonAsync<List<TodoItem>>("/todo");
            Assert.Single(items);
            Assert.Equal("Test Item #1", items[0].Description);
            Assert.Equal(TodoItemStatus.New, items[0].Status);
            Assert.True(items[0].Id.HasValue);

        }
    }
}
