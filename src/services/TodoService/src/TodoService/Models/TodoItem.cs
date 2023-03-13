using System;
using System.ComponentModel;

namespace TodoService.Models
{
    /// <summary>
    /// Represents a todo item.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// The unique id of the item.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The due date of the item.
        /// </summary>
        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        /// The status of the item.
        /// </summary>
        public TodoItemStatus? Status { get; set; }

    }
}
