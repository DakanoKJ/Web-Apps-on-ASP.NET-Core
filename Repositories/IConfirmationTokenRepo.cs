using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IConfirmationTokenRepo : IRepo<ConfirmationTokenModel>
{
    Task<List<ConfirmationTokenModel>> GetAllByAccountIdAsync(int accountId);
    Task ConfirmAsync(int id, DateTime confirmedAt);
}