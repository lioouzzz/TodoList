using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;


        [Required(ErrorMessage = "UserId 為必填")]
        public string UserId { get; set; } = string.Empty;


        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
