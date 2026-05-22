using System.Security.Cryptography;
using System.Text;
using PersonalAccount.Models;
using PersonalAccount.Repository;

namespace PersonalAccount.Services.Auth;

public class ConfirmationTokenTokenService(IConfirmationTokenRepo confirmationTokens) : IConfirmationTokenService
{
    public async Task<string> GenerateTokenAsync(int accountId)
    {
        var token = Guid.NewGuid().ToString();
        var confirmation = new ConfirmationTokenModel
        {
            AccountId = accountId,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            TokenHash = HashToken(token),
        };
        await confirmationTokens.AddAsync(confirmation);
        return token;
    }

    public async Task<bool> ValidateTokenAsync(int accountId, string token)
    {
        var confirmations = await confirmationTokens.GetByAccountIdAsync(accountId);
        var tokenHash = HashToken(token);
        var confirmation = confirmations.FirstOrDefault(confirmation =>
            confirmation.TokenHash == tokenHash
            && confirmation.ExpiresAt > DateTime.UtcNow
            && confirmation.ConfirmedAt is null);

        if (confirmation is null) return false;

        try
        {
            await confirmationTokens.ConfirmAsync(confirmation.Id);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> HasConfirmedTokensAsync(int accountId)
    {
        var confirmations = await confirmationTokens.GetByAccountIdAsync(accountId);
        return confirmations.Any(confirmation => confirmation.ConfirmedAt is not null);
    }

    private static string HashToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}