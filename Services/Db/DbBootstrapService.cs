using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repository.Mappers;
using PersonalAccount.Types;

namespace PersonalAccount.Services.Db;

public class DbBootstrapService(
    AppDbContext context,
    IMapper<AccountEntity, AccountModel> accountMapper,
    IPasswordHasher<AccountModel> hasher,
    IOptions<DbBootstrapSettings> options)
{
    private readonly DbBootstrapSettings _settings = options.Value;

    public async Task SeedAsync()
    {
        var hasStudents = await context.Accounts.AnyAsync();
        if (hasStudents) return;

        var account = new AccountModel
        {
            Email = _settings.Email,
            Role = AccountRole.Administrator,
        };

        var accountEntity = accountMapper.ToEntity(account);
        accountEntity.PasswordHash = hasher.HashPassword(account, _settings.Password);

        await context.Accounts.AddAsync(accountEntity);
        await context.SaveChangesAsync();
    }
}
