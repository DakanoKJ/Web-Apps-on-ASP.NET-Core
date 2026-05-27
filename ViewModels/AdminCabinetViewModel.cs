namespace PersonalAccount.ViewModels;

public class AdminCabinetGroupInfoViewModel : ViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
}

public class AdminCabinetStudentInfoViewModel : ViewModel
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetViewModel : ViewModel
{
    public Dictionary<AdminCabinetGroupInfoViewModel, List<AdminCabinetStudentInfoViewModel>> GroupInfos { get; set; } =
        [];
}