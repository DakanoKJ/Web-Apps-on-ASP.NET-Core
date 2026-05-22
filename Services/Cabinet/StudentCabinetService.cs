using PersonalAccount.Models;
using PersonalAccount.Repository;

namespace PersonalAccount.Services.Cabinet;

public class StudentCabinetService(IStudentProfileRepo studentProfiles) : IStudentCabinetService
{
    public async Task<StudentProfileModel?> GetStudentAsync(int accountId) =>
        await studentProfiles.GetByAccountIdAsync(accountId);

    public async Task<List<StudentProfileModel>> GetAllStudentsAsync(int accountId) =>
        await studentProfiles.GetAllAsync();
}