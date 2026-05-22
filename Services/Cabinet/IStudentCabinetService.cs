using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public interface IStudentCabinetService
{
    Task<StudentProfileModel?> GetStudentAsync(int accountId);
}