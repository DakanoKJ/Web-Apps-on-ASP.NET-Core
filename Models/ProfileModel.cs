namespace PersonalAccount.Models;

public abstract class ProfileModel : Model
{
    public int AccountId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public Uri? PhotoUrl { get; set; }
    
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        obj is ProfileModel
        && base.Equals(obj);
}