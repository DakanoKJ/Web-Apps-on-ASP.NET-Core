using System.Linq.Expressions;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IRepo<TEntity, TModel>
    where TEntity : Entity, new()
    where TModel : Model, new()
{
    // CREATE
    Task AddAsync(TModel model);

    // READ
    Task<TModel?> GetByIdAsync(int id);
    Task<List<TModel>> GetAllAsync();
    Task<bool> AnyAsync();
    Task<bool> ContainsByIdAsync(int id);
}