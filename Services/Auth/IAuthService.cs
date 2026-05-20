using PersonalAccount.Models;

namespace PersonalAccount.Services.Auth
{
    public interface IAuthService
    {
        Task<AccountModel?> ValidateCredentialsAsync(string email, string password);
        Task SignInAsync(HttpContext ctx, AccountModel account);
        Task SignOutAsync(HttpContext ctx);
    }
}
