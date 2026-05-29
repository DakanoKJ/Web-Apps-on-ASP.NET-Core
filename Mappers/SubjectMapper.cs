using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Mappers;

public class SubjectMapper : Mapper<SubjectEntity, SubjectModel>
{
    public override SubjectEntity ToEntity(SubjectModel model)
    {
        var entity = base.ToEntity(model);
        entity.Name = model.Name;
        return entity;
    }

    public override SubjectModel ToModel(SubjectEntity entity)
    {
        var model = base.ToModel(entity);
        model.Name = entity.Name;
        return model;
    }
}

public class TeacherGroupSubjectMapper : Mapper<TeacherGroupSubjectEntity, TeacherGroupSubjectModel>
{
    public override TeacherGroupSubjectEntity ToEntity(TeacherGroupSubjectModel model)
    {
        var entity = base.ToEntity(model);
        entity.GroupId = model.GroupId;
        entity.TeacherAccountId = model.TeacherAccountId;
        entity.SubjectId = model.SubjectId;
        return entity;
    }

    public override TeacherGroupSubjectModel ToModel(TeacherGroupSubjectEntity entity)
    {
        var model = base.ToModel(entity);
        model.GroupId = entity.GroupId;
        model.TeacherAccountId = entity.TeacherAccountId;
        model.SubjectId = entity.SubjectId;
        return model;
    }
}