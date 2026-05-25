namespace PersonalAccount.Services.Account;

public interface IConfirmationTokenService
{
    Task<string> GenerateTokenAsync(int accountId);
    Task<bool> ValidateTokenAsync(int accountId, string token);
    Task<bool> HasConfirmedTokensAsync(int accountId);
}