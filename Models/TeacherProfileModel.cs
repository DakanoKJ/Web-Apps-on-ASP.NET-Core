namespace PersonalAccount.Models;

public class TeacherProfileModel : ProfileModel
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public override bool Equals(object? obj) =>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        obj is TeacherProfileModel
        && base.Equals(obj);
}