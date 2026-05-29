namespace PersonalAccount.Models;

public class TeacherGroupSubjectModel : Model
{
    public int TeacherAccountId { get; set; }
    public int GroupId { get; set; }
    public int SubjectId { get; set; }
    
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        obj is TeacherGroupSubjectModel
        && base.Equals(obj);
}