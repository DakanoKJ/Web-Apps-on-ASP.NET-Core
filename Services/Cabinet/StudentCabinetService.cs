using PersonalAccount.Models.Students;
using PersonalAccount.Repository;

namespace PersonalAccount.Services.Cabinet;

public class StudentCabinetService(IStudentRepo<StudentModel> students, IConfirmationTokenRepo confirmationTokens) : IStudentCabinetService
{
    public Task<StudentModel?> GetStudentByIdAsync(int id) => students.GetByIdAsync(id);
}