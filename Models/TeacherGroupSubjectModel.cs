namespace PersonalAccount.Models;

public class TeacherGroupSubjectModel : Model
{
    public int TeacherAccountId { get; set; }
    public int GroupId { get; set; }
    public int SubjectId { get; set; }
    
    public override bool Equals(object? obj) =>
        obj is TeacherGroupSubjectModel
        && base.Equals(obj);
}