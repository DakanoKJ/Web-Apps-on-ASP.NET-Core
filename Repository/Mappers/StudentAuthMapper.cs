using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Utils;

namespace PersonalAccount.Repository.Mappers;

public class StudentAuthMapper : IMapper<StudentEntity, StudentAuthModel>
{
    public StudentEntity? ToEntity(StudentAuthModel? model) => model == null
        ? null
        : new StudentEntity
        {
            Id = model.Id,
            Email = model.Email,
            PhotoUrl = model.PhotoUrl?.ToString(),
            FullName =  model.FullName,
            GroupName =  model.GroupName,
            PasswordHash =  model.PasswordHash,
        };

    public StudentAuthModel? ToModel(StudentEntity? entity) => entity == null
        ? null
        : new StudentAuthModel
        {
            Id = entity.Id,
            Email = entity.Email,
            PhotoUrl = entity.PhotoUrl?.ToUri(),
            FullName =  entity.FullName,
            GroupName =  entity.GroupName,
            PasswordHash =  entity.PasswordHash,
        };
}