using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class TeacherGroupSubjectRepo(
    AppDbContext ctx,
    IMapper<TeacherGroupSubjectEntity, TeacherGroupSubjectModel> mapper)
    : Repo<TeacherGroupSubjectEntity, TeacherGroupSubjectModel>(ctx, mapper, c => c.TeacherGroupSubjects),
        ITeacherGroupSubjectRepo
{
    public async Task<List<TeacherGroupSubjectModel>> GetAllByTeacherAccountIdAsync(int teacherAccountId) =>
        await GetAllByAsync(entity => entity.TeacherAccountId == teacherAccountId);
}