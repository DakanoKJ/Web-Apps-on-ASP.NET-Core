namespace PersonalAccount.Models;

public class SubjectModel : Model
{
    public string Name { get; set; } = string.Empty;
    
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        obj is SubjectModel
        && base.Equals(obj);
}