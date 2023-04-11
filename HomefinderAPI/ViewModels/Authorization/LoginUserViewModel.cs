using System.ComponentModel.DataAnnotations;

namespace HomefinderAPI.ViewModels.Authorization
{
  public class LoginUserViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}