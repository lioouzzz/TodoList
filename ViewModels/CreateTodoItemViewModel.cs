using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class CreateTodoItemViewModel
    {
        [Required(ErrorMessage = "標題為必填")]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
