using PersonalAccount.Models;

namespace PersonalAccount.Repository;

public interface IConfirmationTokenRepo
{
    Task<List<ConfirmationTokenModel>> GetByStudentIdAsync(int studentId);
    Task AddAsync(ConfirmationTokenModel token);
    Task ConfirmAsync(int id);
}