using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public interface ITeacherCabinetService
{
    Task<TeacherProfileModel?> GetTeacherProfileAsync(int teacherAccountId);
    Task<List<TeacherGroupSubjectModel>> GetAllTeacherGroupSubjectsAsync(int teacherAccountId);
    Task<List<SubjectModel>> GetAllSubjects(List<TeacherGroupSubjectModel> teacherGroupSubjects);
    Task<Dictionary<int, List<GroupModel>>> GetAllGroupsBySubjects(List<TeacherGroupSubjectModel> teacherGroupSubjects);
}