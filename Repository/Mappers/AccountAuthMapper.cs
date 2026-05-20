using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Repository.Mappers;

public class AccountAuthMapper : IMapper<AccountEntity, AccountModel>
{
    public AccountEntity ToEntity(AccountModel model) =>
        new()
        {
            Id = model.Id,
            Email = model.Email,
            Role = model.Role,
            PasswordHash = model.PasswordHash,
        };

    public AccountModel ToModel(AccountEntity entity) =>
        new()
        {
            Id = entity.Id,
            Email = entity.Email,
            Role = entity.Role,
            PasswordHash = entity.PasswordHash,
        };
}