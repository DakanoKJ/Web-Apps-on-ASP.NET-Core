using System.Security.Cryptography;
using System.Text;
using PersonalAccount.Models;
using PersonalAccount.Repository;

namespace PersonalAccount.Services.Auth;

public class ConfirmationTokenTokenService(IConfirmationTokenRepo confirmationTokens) : IConfirmationTokenService
{
    public async Task<string> GenerateTokenAsync(int studentId)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var confirmation = new ConfirmationTokenModel
        {
            StudentId = studentId,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            TokenHash = HashToken(token),
        };
        await confirmationTokens.AddAsync(confirmation);
        return token;
    }

    public async Task<bool> ValidateTokenAsync(int studentId, string token)
    {
        var confirmations = await confirmationTokens.GetByStudentIdAsync(studentId);
        var confirmation = confirmations.FirstOrDefault(confirmation =>
            confirmation.TokenHash == HashToken(token)
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

    public async Task<bool> HasConfirmedTokensAsync(int studentId)
    {
        var confirmations = await confirmationTokens.GetByStudentIdAsync(studentId);
        return confirmations.Any(confirmation => confirmation.ConfirmedAt is not null);
    }

    private static string HashToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}