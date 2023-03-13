using System.Linq;

namespace TodoService.Models
{
    public static class TodoContextExtensions
    {
        public static bool TodoItemExists(this TodoContext context, long id) => context.TodoItems.Any(e => e.Id == id);
    }
}
