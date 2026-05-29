using PersonalAccount.Models;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services.Cabinet;

public class TeacherCabinetService(
    IGroupRepo groupRepo,
    ISubjectRepo subjectRepo,
    ITeacherGroupSubjectRepo teacherGroupSubjectRepo
) : ITeacherCabinetService
{
    public async Task<List<TeacherGroupSubjectModel>> GetAllTeacherGroupSubjectsAsync(int teacherAccountId) =>
        await teacherGroupSubjectRepo.GetAllByTeacherAccountIdAsync(teacherAccountId);

    public async Task<List<SubjectModel>> GetAllSubjects(List<TeacherGroupSubjectModel> teacherGroupSubjects)
    {
        var subjects = new HashSet<SubjectModel>();
        foreach (var teacherGroupSubject in teacherGroupSubjects)
        {
            var subject = await subjectRepo.GetByIdAsync(teacherGroupSubject.SubjectId);
            if (subject == null) throw new KeyNotFoundException();
            subjects.Add(subject);
        }

        return subjects.ToList();
    }

    public async Task<Dictionary<int, List<GroupModel>>> GetAllGroupsBySubjects(List<TeacherGroupSubjectModel> teacherGroupSubjects)
    {
        var groupsBySubject = new Dictionary<int, List<GroupModel>>();
        foreach (var teacherGroupSubject in teacherGroupSubjects)
        {
            if (!groupsBySubject.ContainsKey(teacherGroupSubject.SubjectId))
                groupsBySubject[teacherGroupSubject.SubjectId] = [];
            var group = await groupRepo.GetByIdAsync(teacherGroupSubject.GroupId);
            if (group == null) throw new KeyNotFoundException();
            groupsBySubject[teacherGroupSubject.SubjectId].Add(group);
        }

        return groupsBySubject;
    }
}