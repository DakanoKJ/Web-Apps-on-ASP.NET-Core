namespace PersonalAccount.Data.Entities;

public class TeacherGroupSubjectEntity : Entity
{
    public int TeacherAccountId { get; set; }
    public AccountEntity? TeacherAccount { get; set; }

    public int GroupId { get; set; }
    public GroupEntity? Group { get; set; }

    public int SubjectId { get; set; }
    public SubjectEntity? Subject { get; set; }
}