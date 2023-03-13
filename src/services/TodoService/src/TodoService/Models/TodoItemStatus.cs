using System.Runtime.Serialization;

namespace TodoService.Models
{
    /// <summary>
    /// The status of a todo item.
    /// </summary>
    public enum TodoItemStatus
    {
        /// <summary>
        /// The item is newly created.
        /// </summary>
        [EnumMember(Value = "New")]
        New,

        /// <summary>
        /// The item is active.
        /// </summary>
        [EnumMember(Value = "Active")]
        Active,

        /// <summary>
        /// The item was completed.
        /// </summary>
        [EnumMember(Value = "Completed")]
        Completed,

        /// <summary>
        /// The item was archived.
        /// </summary>
        [EnumMember(Value = "Archived")]
        Archived
    }
}
