
using Microsoft.EntityFrameworkCore;
using TodoService.Models;

namespace TodoService.Models
{
    /// <summary>
    /// A database context for todo items.
    /// </summary>
    public class TodoContext : DbContext
    {
        /// <summary>
        /// Constructs a new context.
        /// </summary>
        /// <param name="options">The set of context options.</param>
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The set of todo items.
        /// </summary>
        public virtual DbSet<TodoItem> TodoItems { get; set; }
    }

}
