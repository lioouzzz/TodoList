using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="確認密碼")]
        [Compare("Password",ErrorMessage ="密碼與確認密碼不一致")]
        public string ConfirmPassword { get; set; }
    }
}
