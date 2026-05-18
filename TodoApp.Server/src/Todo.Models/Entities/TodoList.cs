using MayNghien.Infrastructures.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.Entities
{
    public class TodoList : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("User")]
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<TodoItem>? TodoItems { get; set; }
    }
}
