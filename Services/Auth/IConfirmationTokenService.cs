namespace PersonalAccount.Services.Auth;

public interface IConfirmationTokenService
{
    Task<string> GenerateTokenAsync(int studentId);
    Task<bool> ValidateTokenAsync(int studentId, string token);
}