using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public interface IStudentCabinetService
{
    Task<StudentProfileModel?> GetStudentProfileAsync(int accountId);
    Task<GroupModel?> GetGroupAsync(int groupId);
}