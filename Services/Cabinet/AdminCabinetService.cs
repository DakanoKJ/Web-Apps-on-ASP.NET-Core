using PersonalAccount.Models;
using PersonalAccount.Repository;
using PersonalAccount.Services.Auth;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(
    IStudentProfileRepo studentProfiles,
    IAccountRepo accounts,
    IConfirmationTokenService confirmations) : IAdminCabinetService
{
    public async Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync() =>
        (await accounts.GetByRoleAsync(AccountRole.Student)).ToDictionary(account => account.Id);

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync() =>
        await studentProfiles.GetAllAsync();

    public async Task ConfirmStudentEmailAsync(int accountId)
    {
        var token = await confirmations.GenerateTokenAsync(accountId);
        await confirmations.ValidateTokenAsync(accountId, token);
    }
}