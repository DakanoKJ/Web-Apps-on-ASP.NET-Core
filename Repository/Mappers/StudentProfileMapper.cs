using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Repository.Mappers;

public class StudentProfileMapper : IMapper<StudentProfileEntity, StudentProfileModel>
{
    public StudentProfileEntity ToEntity(StudentProfileModel profileModel) =>
        new()
        {
            Id = profileModel.Id,
            AccountId = profileModel.AccountId,
            PhotoUrl = profileModel.PhotoUrl?.ToString(),
            FullName = profileModel.FullName,
            GroupName = profileModel.GroupName,
        };

    public StudentProfileModel ToModel(StudentProfileEntity entity) =>
        new()
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            PhotoUrl = entity.PhotoUrl?.ToUri(),
            FullName = entity.FullName,
            GroupName = entity.GroupName,
        };
}