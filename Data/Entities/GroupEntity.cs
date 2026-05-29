namespace PersonalAccount.Data.Entities;

public class GroupEntity : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

    public List<StudentProfileEntity> StudentProfiles { get; set; } = [];
    public List<TeacherGroupSubjectEntity> TeacherGroupSubjects { get; set; } = [];
}