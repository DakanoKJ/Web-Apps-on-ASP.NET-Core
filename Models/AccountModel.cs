using PersonalAccount.Types;

namespace PersonalAccount.Models;

public class AccountModel
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public AccountRole Role { get; set; } = AccountRole.Student;
}