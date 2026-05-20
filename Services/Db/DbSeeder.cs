using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Repository.Mappers;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Db;

public class DbSeederSettings
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class DbSeeder(
    AppDbContext context,
    IMapper<StudentEntity, StudentAuthModel> mapper,
    IPasswordHasher<StudentAuthModel> hasher,
    IOptions<DbSeederSettings> options)
{
    private readonly DbSeederSettings _settings = options.Value;

    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();

        var hasStudents = await context.Students.AnyAsync();
        if (hasStudents) return;

        var model = new StudentAuthModel
        {
            FullName = "John Doe",
            GroupName = "P-318",
            Email = _settings.Email,
            PhotoUrl =
                "https://murkosha.ru/sites/default/files/styles/adaptive/public/news/2022/nikitis_i_sedrik.jpg?itok=KOsbDKW6"
                    .ToUri()
        };

        var entity = mapper.ToEntity(model)!;
        entity.PasswordHash = hasher.HashPassword(model, _settings.Password);

        await context.Students.AddAsync(entity);
        await context.SaveChangesAsync();
    }
}