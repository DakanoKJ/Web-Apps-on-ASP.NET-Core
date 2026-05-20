using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repository.Mappers;

namespace PersonalAccount.Services.Db;

public class DbBootstrapService(
    AppDbContext context,
    IMapper<AccountEntity, AccountModel> mapper,
    IPasswordHasher<AccountModel> hasher,
    IOptions<DbBootstrapSettings> options)
{
    private readonly DbBootstrapSettings _settings = options.Value;

    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();

        var hasStudents = await context.Accounts.AnyAsync();
        if (hasStudents) return;

        var model = new AccountModel
        {
            Email = _settings.Email,
        };

        var entity = mapper.ToEntity(model)!;
        entity.PasswordHash = hasher.HashPassword(model, _settings.Password);

        await context.Accounts.AddAsync(entity);
        await context.SaveChangesAsync();
    }
}