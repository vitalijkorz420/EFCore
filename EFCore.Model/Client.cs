namespace EFCore.Model;
public class Client
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public List<Order> Orders { get; } = new();
}
