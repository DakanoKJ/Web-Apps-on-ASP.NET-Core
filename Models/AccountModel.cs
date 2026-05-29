using PersonalAccount.Types;

namespace PersonalAccount.Models;

public class AccountModel : Model
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public AccountRoles Role { get; set; } = AccountRoles.Student;

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        obj is AccountModel 
        && base.Equals(obj);
}