using PersonalAccount.Models;
using PersonalAccount.Types;

namespace PersonalAccount.Repositories
{
    public interface IAccountRepo
    {
        public Task<AccountModel?> GetByEmailAsync(string email);
        public Task<List<AccountModel>> GetByRoleAsync(AccountRoles roles);
        public Task AddAccountAsync(AccountModel account);
        public Task<bool> AnyAsync();
    }
}
