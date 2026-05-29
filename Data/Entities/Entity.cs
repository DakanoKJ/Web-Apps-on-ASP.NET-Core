namespace PersonalAccount.Data.Entities;

public class Entity
{
    public int Id { get; set; }
}


public class ProfileEntity : Entity
{
    public int AccountId { get; set; }
    public AccountEntity? Account { get; set; }
    
    public string FullName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}