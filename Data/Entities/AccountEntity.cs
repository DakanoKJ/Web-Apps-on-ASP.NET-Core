using PersonalAccount.Types;

namespace PersonalAccount.Data.Entities;

public class AccountEntity
{
    public int Id { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public AccountRole Role { get; set; } = AccountRole.Student;

    public StudentProfileEntity? StudentProfile { get; set; }
    public List<ConfirmationTokenEntity> ConfirmationTokens { get; set; } = [];
}