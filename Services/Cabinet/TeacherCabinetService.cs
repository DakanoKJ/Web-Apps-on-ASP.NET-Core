using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public class TeacherCabinetService : ITeacherCabinetService
{
    public Task<Dictionary<int, List<GroupModel>>> GetAllTeacherGroupsGroupedBySubjectsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Dictionary<int, List<SubjectModel>>> GetAllTeacherSubjectsGroupedByGroupsAsync()
    {
        throw new NotImplementedException();
    }
}