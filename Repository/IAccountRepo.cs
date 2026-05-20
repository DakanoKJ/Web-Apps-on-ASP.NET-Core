using PersonalAccount.Models;

namespace PersonalAccount.Repository
{
    public interface IAccountRepo
    {
        public Task<AccountModel?> GetByEmailAsync(string email);
        public Task<AccountModel?> GetByIdAsync(int id);
    }
}
