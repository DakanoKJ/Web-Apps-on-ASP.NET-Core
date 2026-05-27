namespace PersonalAccount.Models;

public class StudentProfileModel : Model
{
    public int AccountId { get; set; }
    public int? GroupId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public Uri? PhotoUrl { get; set; }
}