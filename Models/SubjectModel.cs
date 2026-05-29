namespace PersonalAccount.Models;

public class SubjectModel : Model
{
    public string Name { get; set; } = string.Empty;
    
    public override bool Equals(object? obj) =>
        obj is SubjectModel
        && base.Equals(obj);
}