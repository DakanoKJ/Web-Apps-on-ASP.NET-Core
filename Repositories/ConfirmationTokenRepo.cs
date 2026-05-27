using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class ConfirmationTokenRepo(
    AppDbContext ctx,
    IMapper<ConfirmationTokenEntity, ConfirmationTokenModel> mapper
) : Repo<ConfirmationTokenEntity, ConfirmationTokenModel>(ctx, mapper, c => c.ConfirmationTokens),
    IConfirmationTokenRepo
{
    public async Task<List<ConfirmationTokenModel>> GetAllByAccountIdAsync(int accountId) =>
        await GetAllByAsync(entity => entity.AccountId == accountId);

    public async Task ConfirmAsync(int id, DateTime confirmedAt) =>
        await UpdateByIdAsync(id, entity => entity.ConfirmedAt = confirmedAt);
}