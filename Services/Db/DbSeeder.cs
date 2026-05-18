using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Repository.Mappers;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Db;

public class DbSeeder(AppDbContext  context, IMapper<StudentEntity, StudentAuthModel> mapper, IPasswordHasher<StudentAuthModel> hasher)
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
        
        var hasStudents = await context.Students.AnyAsync();
        if (hasStudents) return;

        var model = new StudentAuthModel
        {
            FullName = "John Doe",
            GroupName = "P-318",
            Email = "example@top",
            PhotoUrl =
                "https://murkosha.ru/sites/default/files/styles/adaptive/public/news/2022/nikitis_i_sedrik.jpg?itok=KOsbDKW6"
                    .ToUri()
        };
        
        var entity = mapper.ToEntity(model)!;
        entity.PasswordHash = hasher.HashPassword(model, "example");
        
        await context.Students.AddAsync(entity);
        await context.SaveChangesAsync();
    }
}