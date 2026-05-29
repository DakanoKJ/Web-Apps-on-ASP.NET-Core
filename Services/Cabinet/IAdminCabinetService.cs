using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public interface IAdminCabinetService
{
    Task<List<AccountModel>> GetAllStudentAccountsAsync();
    Task<List<StudentProfileModel>> GetAllStudentProfilesAsync();
    Task<List<GroupModel>> GetAllGroupsAsync();
    Task AddStudentProfileAsync(string email, string fullName);
    Task AddGroupAsync(string groupName, string description = "", Uri? imageUrl = null);
}