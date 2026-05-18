namespace PersonalAccount.Models.Students
{
    public class StudentAuthModel : StudentModel
    {
        public string PasswordHash { get; set; } = string.Empty;
    }
}
