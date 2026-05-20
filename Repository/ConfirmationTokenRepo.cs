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

    public async Task<List<ConfirmationTokenModel>> GetByAccountIdAsync(int accountId) =>
        await ConfirmationTokens
            .AsNoTracking()
            .Where(entity => entity.AccountId == accountId)
            .Select(entity => mapper.ToModel(entity))
            .ToListAsync();

    public async Task AddAsync(ConfirmationTokenModel token)
    {
        await ConfirmationTokens.AddAsync(mapper.ToEntity(token));
        await context.SaveChangesAsync();
    }

    public async Task ConfirmAsync(int id)
    {
        var token = await ConfirmationTokens.FindAsync(id) ?? throw new KeyNotFoundException();
        token.ConfirmedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}