namespace PersonalAccount.Data.Entities;

public class SubjectEntity : Entity
{
    public string Name { get; set; } = string.Empty;
    public List<TeacherGroupSubjectEntity> TeacherGroupSubjects { get; set; } = [];
}