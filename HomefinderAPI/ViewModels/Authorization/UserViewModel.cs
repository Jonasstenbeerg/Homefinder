namespace HomefinderAPI.ViewModels.Authorization
{
  public class UserViewModel
  {
    public string? Username { get; set; }
    public string? Token { get; set; }
    public DateTime Expires { get; set; }
  }
}