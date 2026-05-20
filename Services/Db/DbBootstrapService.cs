using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repository.Mappers;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Db;

public class DbBootstrapService(
    AppDbContext context,
    IMapper<AccountEntity, AccountModel> accountMapper,
    IMapper<StudentProfileEntity, StudentProfileModel> studentProfileMapper,
    IPasswordHasher<AccountModel> hasher,
    IOptions<DbBootstrapSettings> options)
{
    private readonly DbBootstrapSettings _settings = options.Value;

    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();

        var hasStudents = await context.Accounts.AnyAsync();
        if (hasStudents) return;

        var account = new AccountModel
        {
            Email = _settings.Email,
        };

        var accountEntity = accountMapper.ToEntity(account);
        accountEntity.PasswordHash = hasher.HashPassword(account, _settings.Password);

        await context.Accounts.AddAsync(accountEntity);
        await context.SaveChangesAsync();
        
        accountEntity = await context.Accounts.AsNoTracking().FirstOrDefaultAsync(entity => entity.Email == account.Email);

        var studentProfile = new StudentProfileModel
        {
            AccountId = accountEntity.Id,
            FullName = "John Doe",
            GroupName = "P-318",
            PhotoUrl =
                "https://img.magnific.com/free-photo/view-beautiful-persian-domestic-cat_23-2151773821.jpg?semt=ais_hybrid&w=740&q=80"
                    .ToUri(),
        };
        
        var studentProfileEntity = studentProfileMapper.ToEntity(studentProfile);
        await context.StudentProfiles.AddAsync(studentProfileEntity);

        await context.SaveChangesAsync();
    }
}