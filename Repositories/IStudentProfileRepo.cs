using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IStudentProfileRepo : IRepo<StudentProfileEntity, StudentProfileModel>
{
    Task<StudentProfileModel?> GetByAccountIdAsync(int accountId);
}