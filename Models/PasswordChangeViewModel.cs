using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Models;

public class PasswordChangeViewModel
{
    [Required(ErrorMessage = "Текущий пароль обязателен")]
    public string OldPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Новый пароль обязателен")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Подтверждение пароля обязательно")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
