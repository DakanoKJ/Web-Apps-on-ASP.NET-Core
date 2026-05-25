using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repository.Mappers;

public class AccountMapper : IMapper<AccountEntity, AccountModel>
{
    public AccountEntity ToEntity(AccountModel model) =>
        new()
        {
            Id = model.Id,
            Email = model.Email,
            Roles = model.Roles,
            PasswordHash = model.PasswordHash,
        };

    public AccountModel ToModel(AccountEntity entity) =>
        new()
        {
            Id = entity.Id,
            Email = entity.Email,
            Roles = entity.Roles,
            PasswordHash = entity.PasswordHash,
        };
}