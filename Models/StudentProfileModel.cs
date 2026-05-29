using PersonalAccount.Constants;

namespace PersonalAccount.Models;

public class StudentProfileModel : ProfileModel
{
    public int GroupId { get; set; } = GroupConstants.NoGroup.Id;
    
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        obj is StudentProfileModel
        && base.Equals(obj);
}