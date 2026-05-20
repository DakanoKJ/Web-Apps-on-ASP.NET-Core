using PersonalAccount.Models;
using PersonalAccount.Repository;

namespace PersonalAccount.Services.Cabinet;

public class StudentCabinetService(IStudentProfileRepo studentProfiles) : IStudentCabinetService
{
    public Task<StudentProfileModel?> GetStudentByAccountIdAsync(int accountId) =>
        studentProfiles.GetByAccountIdAsync(accountId);
}