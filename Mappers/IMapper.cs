using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Mappers;

public interface IMapper<TEntity, TModel>
    where TEntity : Entity
    where TModel : Model
{
    TEntity ToEntity(TModel model);
    TModel ToModel(TEntity entity);
}