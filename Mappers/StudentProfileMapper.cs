using PersonalAccount.Constants;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Mappers;

public class StudentProfileMapper : IMapper<StudentProfileEntity, StudentProfileModel>
{
    public StudentProfileEntity ToEntity(StudentProfileModel model) =>
        new()
        {
            Id = model.Id,
            AccountId = model.AccountId,
            PhotoUrl = model.PhotoUrl?.ToString(),
            FullName = model.FullName,
            GroupId = model.GroupId == GroupConstants.NoGroup.Id ? null : model.GroupId,
        };

    public StudentProfileModel ToModel(StudentProfileEntity entity) =>
        new()
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            PhotoUrl = entity.PhotoUrl?.ToUri(),
            FullName = entity.FullName,
            GroupId = entity.GroupId ?? GroupConstants.NoGroup.Id,
        };
}