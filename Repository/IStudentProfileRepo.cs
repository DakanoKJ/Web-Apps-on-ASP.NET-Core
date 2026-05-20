using PersonalAccount.Models;

namespace PersonalAccount.Repository;

public interface IStudentProfileRepo
{
    Task<StudentProfileModel?> GetByAccountIdAsync(int accountId);
}