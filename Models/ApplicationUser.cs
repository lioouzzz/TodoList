using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ToDo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

    }
}
