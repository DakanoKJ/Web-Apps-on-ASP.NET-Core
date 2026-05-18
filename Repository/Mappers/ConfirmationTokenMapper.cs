using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repository.Mappers;

public class ConfirmationTokenMapper : IMapper<ConfirmationTokenEntity, ConfirmationTokenModel>
{
    public ConfirmationTokenEntity? ToEntity(ConfirmationTokenModel? model) => model == null
        ? null
        : new ConfirmationTokenEntity
        {
            Id = model.Id,
            StudentId = model.StudentId,
            TokenHash = model.TokenHash,
            ConfirmedAt = model.ConfirmedAt,
            ExpiresAt = model.ExpiresAt,
        };

    public ConfirmationTokenModel? ToModel(ConfirmationTokenEntity? entity) => entity == null
        ? null
        : new ConfirmationTokenModel
        {
            Id = entity.Id,
            StudentId = entity.StudentId,
            TokenHash = entity.TokenHash,
            ConfirmedAt = entity.ConfirmedAt,
            ExpiresAt = entity.ExpiresAt,
        };
}