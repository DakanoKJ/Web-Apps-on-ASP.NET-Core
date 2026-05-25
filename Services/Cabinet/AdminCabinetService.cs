using PersonalAccount.Models;
using PersonalAccount.Repositories;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(IStudentProfileRepo studentProfileRepo, IAccountRepo accountRepo, IGroupRepo groupRepo)
    : IAdminCabinetService
{
    public async Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync() =>
        (await accountRepo.GetByRoleAsync(AccountRoles.Student)).ToDictionary(account => account.Id);

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync() => await studentProfileRepo.GetAllAsync();

    public async Task<Dictionary<int, GroupModel>> GetAllGroupsAsync() =>
        (await groupRepo.GetAllAsync()).ToDictionary(group => group.Id);

    public async Task AddStudentProfileAsync(string email, string fullName)
    {
        var account = await accountRepo.GetByEmailAsync(email);
        if (account == null) return;

        var profile = new StudentProfileModel
        {
            FullName = fullName,
            AccountId = account.Id
        };

        await studentProfileRepo.AddAsync(profile);
    }

    public async Task AddGroupAsync(string groupName, string description = "", Uri? imageUrl = null) =>
        await groupRepo.AddAsync(new GroupModel
        {
            Name = groupName,
            Description = description,
            ImageUrl = imageUrl
        });
}