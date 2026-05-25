using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class GroupRepo(AppDbContext context, IMapper<GroupEntity, GroupModel> mapper) : IGroupRepo
{
    private DbSet<GroupEntity> Groups => context.Groups;

    public async Task<List<GroupModel>> GetAllAsync() => await Groups
        .AsNoTracking()
        .Select(entity => mapper.ToModel(entity))
        .ToListAsync();

    public async Task AddAsync(GroupModel group)
    {
        await Groups.AddAsync(mapper.ToEntity(group));
        await context.SaveChangesAsync();
    }

    public async Task<GroupModel?> GetByIdAsync(int id)
    {
        var entity = await Groups.FindAsync(id);
        return entity == null ? null : mapper.ToModel(entity);
    }
}