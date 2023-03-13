using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TodoService.Models;
using System;
using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace TodoService
{
    public class TodoServiceTest
    {
        private Stack<Action> actions;

        public TodoServiceTest()
        {
        }

        [Fact]
        public async Task FindAll_ItemsArePresent()
        {
            await Task.CompletedTask;
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
