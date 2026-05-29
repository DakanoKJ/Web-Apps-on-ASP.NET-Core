using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace PersonalAccount.ViewModels;

public class TeacherCabinetGroupInfoViewModel : ViewModel
{
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
}

public class TeacherCabinetSubjectInfoViewModel : ViewModel
{
    public string Name { get; set; } = string.Empty;
}

public class TeacherCabinetViewModel : CabinetViewModel
{
    public List<int> SubjectIdsOrder { get; set; } = [];
    public Dictionary<int, TeacherCabinetSubjectInfoViewModel> SubjectInfos { get; set; } = [];
    public Dictionary<int, List<TeacherCabinetGroupInfoViewModel>> GroupsBySubjectInfos { get; set; } = [];
}