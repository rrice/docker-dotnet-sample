
using System;
using System.Collections.Generic;
using TodoService.Models;

namespace TodoService
{

    internal static class StubFactory
    {
        public static IEnumerable<TodoItem> CreateTodoItems(int count)
        {
            if (count <= 0)
            {
                yield break;
            }
            for (var i = 0; i < count; i++)
            {
                var item = new TodoItem()
                {
                    Description = $"Test Item {i + 1}",
                    DueDate = DateTimeOffset.UtcNow
                        .AddDays(i)
                };
                yield return item;
            }
        }
    }
}
