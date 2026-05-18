using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Utils;

namespace PersonalAccount.Repository.Mappers;

public class StudentMapper : IMapper<StudentEntity, StudentModel>
{
    public StudentEntity? ToEntity(StudentModel? model) => model == null
        ? null
        : new StudentEntity
        {
            Id = model.Id,
            Email = model.Email,
            PhotoUrl = model.PhotoUrl?.ToString(),
            FullName =  model.FullName,
            GroupName =  model.GroupName,
        };

    public StudentModel? ToModel(StudentEntity? entity) => entity == null
        ? null
        : new StudentModel
        {
            Id = entity.Id,
            Email = entity.Email,
            PhotoUrl = entity.PhotoUrl?.ToUri(),
            FullName =  entity.FullName,
            GroupName =  entity.GroupName,
        };
}