using PersonalAccount.Models;
using PersonalAccount.Repository;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(IStudentProfileRepo studentProfiles, IAccountRepo accounts) : IAdminCabinetService
{
    public async Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync() => (await accounts.GetByRoleAsync(AccountRoles.Student)).ToDictionary(account => account.Id);
    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync() => await studentProfiles.GetAllAsync();
}