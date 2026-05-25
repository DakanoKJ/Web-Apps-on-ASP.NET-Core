using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IGroupRepo
{
    Task<List<GroupModel>> GetAllAsync();
    Task AddAsync(GroupModel group);
    Task<GroupModel?> GetByIdAsync(int id);
}