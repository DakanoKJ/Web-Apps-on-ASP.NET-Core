using PersonalAccount.Models.Students;

namespace PersonalAccount.Services.Auth
{
    public interface IStudentAuthService
    {
        Task<StudentModel?> ValidateStudentAsync(string email, string password);
    }
}
