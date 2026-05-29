using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface ITeacherGroupSubjectRepo : IRepo<TeacherGroupSubjectEntity, TeacherGroupSubjectModel>
{
    Task<List<TeacherGroupSubjectModel>> GetAllByTeacherAccountIdAsync(int teacherAccountId);
}