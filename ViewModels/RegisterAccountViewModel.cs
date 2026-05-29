using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.ViewModels;

public class RegisterAccountViewModel : ViewModel
{
    [Required(ErrorMessage = "FullName is required")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "ContactEmail is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string ContactEmail { get; set; } = string.Empty;
}