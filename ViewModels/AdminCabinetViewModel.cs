namespace PersonalAccount.ViewModels;

public class AdminCabinetStudentInfoViewModel
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetViewModel
{
    public List<AdminCabinetStudentInfoViewModel> StudentInfos { get; set; } = [];
}

