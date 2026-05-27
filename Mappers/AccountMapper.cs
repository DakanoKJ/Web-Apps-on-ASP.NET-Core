using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Mappers;

public class AccountMapper : Mapper<AccountEntity, AccountModel>
{
    public override AccountEntity ToEntity(AccountModel model)
    {
        var entity = base.ToEntity(model);
        entity.Email = model.Email;
        entity.Role = model.Role;
        entity.PasswordHash = model.PasswordHash;
        return entity;
    }

    public override AccountModel ToModel(AccountEntity entity)
    {
        var model = base.ToModel(entity);
        model.Email = entity.Email;
        model.Role = entity.Role;
        model.PasswordHash = entity.PasswordHash;
        return model;
    }
}