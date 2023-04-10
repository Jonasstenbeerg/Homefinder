using System.ComponentModel.DataAnnotations;

namespace HomefinderAPI.ViewModels.Authorization
{
  public class RegisterUserViewModel
    {
        [Required(ErrorMessage ="e-post adress är obligatoriskt")]
        [EmailAddress(ErrorMessage="Ogiltig e-post adress")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Lösenord är obligatoriskt")]
        public string? Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}