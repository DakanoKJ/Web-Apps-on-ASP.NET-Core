using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repository.Mappers;

namespace PersonalAccount.Repository;

public class ConfirmationTokenRepo(
    AppDbContext context,
    IMapper<ConfirmationTokenEntity, ConfirmationTokenModel> mapper) : IConfirmationTokenRepo
{
    private DbSet<ConfirmationTokenEntity> ConfirmationTokens => context.ConfirmationTokens;

    public async Task<List<ConfirmationTokenModel>> GetByStudentIdAsync(int studentId)
    {
        var entities = await ConfirmationTokens
            .AsNoTracking()
            .Where(token => token.StudentId == studentId)
            .ToListAsync();

        return entities.Select(entity => mapper.ToModel(entity)!).ToList();
    }

    public async Task AddAsync(ConfirmationTokenModel token)
    {
        await ConfirmationTokens.AddAsync(mapper.ToEntity(token)!);
        await context.SaveChangesAsync();
    }

    public async Task ConfirmAsync(int id)
    {
        var token = await ConfirmationTokens.FindAsync(id) ?? throw new KeyNotFoundException();
        token.ConfirmedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}