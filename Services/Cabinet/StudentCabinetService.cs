using PersonalAccount.Models.Students;
using PersonalAccount.Repository;

namespace PersonalAccount.Services.Cabinet;

public class StudentCabinetService(IStudentRepo<StudentModel> students) : IStudentCabinetService
{
    public Task<StudentModel?> GetStudentByIdAsync(int id) => students.GetByIdAsync(id);

    public Task UpdateByIdAsync(int id, StudentModel student) => students.UpdateByIdAsync(id, student);
}