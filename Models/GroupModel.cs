namespace PersonalAccount.Models;

public class GroupModel : Model
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Uri? ImageUrl { get; set; }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        obj is GroupModel
        && base.Equals(obj);
}