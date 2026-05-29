using PersonalAccount.Types;

namespace PersonalAccount.Models;

public class AccountModel : Model
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public AccountRoles Role { get; set; } = AccountRoles.Student;

    public override bool Equals(object? obj) =>
        obj is AccountModel 
        && base.Equals(obj);
}