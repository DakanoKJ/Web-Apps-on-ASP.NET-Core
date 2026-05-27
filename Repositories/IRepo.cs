using System.Linq.Expressions;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IRepo<TEntity, TModel>
    where TEntity : Entity
    where TModel : Model
{
    // CREATE
    Task AddAsync(TModel model);

    // READ
    Task<TModel?> GetByIdAsync(int id);
    Task<TModel?> GetByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TModel>> GetAllAsync();
    Task<List<TModel>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> AnyAsync();

    // UPDATE
    Task<bool> UpdateByIdAsync(int id, Action<TEntity> update);
    
    // DELETE
    Task<bool> DeleteByIdAsync(int id);
}