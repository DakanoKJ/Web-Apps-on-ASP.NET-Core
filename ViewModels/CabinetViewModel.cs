namespace PersonalAccount.ViewModels;

public abstract class CabinetViewModel : ViewModel
{
    public string? PhotoUrl { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
}