using Microsoft.AspNetCore.Identity;
using PersonalAccount.Models.Students;
using PersonalAccount.Repository;

namespace PersonalAccount.Services.Cabinet;

public class PasswordService(
    IStudentRepo<StudentAuthModel> students,
    IPasswordHasher<StudentAuthModel> hasher) : IPasswordService
{
    public async Task<bool> ValidatePasswordAsync(int id, string password)
    {
        var student = await students.GetByIdAsync(id);
        if (student is null) return false;
        var result = hasher.VerifyHashedPassword(student, student.PasswordHash, password);
        return result != PasswordVerificationResult.Failed;
    }

    public async Task UpdatePasswordAsync(int id, string password)
    {
        var student = await students.GetByIdAsync(id);
        if (student is null) return;
        var hash = hasher.HashPassword(student, password);
        await students.UpdatePasswordHashAsync(id, hash);
    }
}
