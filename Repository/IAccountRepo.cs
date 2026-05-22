using PersonalAccount.Models;
using PersonalAccount.Types;

namespace PersonalAccount.Repository
{
    public interface IAccountRepo
    {
        public Task<AccountModel?> GetByEmailAsync(string email);
        public Task<List<AccountModel>> GetByRoleAsync(AccountRole role);
        public Task<AccountModel?> GetByIdAsync(int id);
    }
}
