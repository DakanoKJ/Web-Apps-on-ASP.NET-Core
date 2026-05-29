using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface ITeacherGroupSubjectRepo : IRepo<TeacherGroupSubjectModel>
{
    Task<List<TeacherGroupSubjectModel>> GetAllByTeacherAccountIdAsync(int teacherAccountId);
}