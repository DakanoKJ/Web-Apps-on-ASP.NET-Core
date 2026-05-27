using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IConfirmationTokenRepo
{
    Task<List<ConfirmationTokenModel>> GetByAccountIdAsync(int accountId);
    Task AddAsync(ConfirmationTokenModel token);
    Task ConfirmAsync(int id);
}