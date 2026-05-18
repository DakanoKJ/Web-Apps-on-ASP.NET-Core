using PersonalAccount.Models.Students;

namespace PersonalAccount.Services.Cabinet;

public interface IStudentCabinetService
{
    Task<StudentModel?> GetStudentByIdAsync(int id);
}