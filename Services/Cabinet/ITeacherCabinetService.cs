using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public interface ITeacherCabinetService
{
    Task<Dictionary<int, List<GroupModel>>> GetAllTeacherGroupsGroupedBySubjectsAsync();
    Task<Dictionary<int, List<SubjectModel>>> GetAllTeacherSubjectsGroupedByGroupsAsync();
}