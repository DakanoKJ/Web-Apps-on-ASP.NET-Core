using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IProfileRepo<TProfileEntity, TProfileModel>
    : IRepo<TProfileEntity, TProfileModel>
    where TProfileEntity : ProfileEntity, new()
    where TProfileModel : ProfileModel, new()
{
    Task<TProfileModel?> GetByAccountIdAsync(int accountId);
}