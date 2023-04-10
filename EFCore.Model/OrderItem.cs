namespace EFCore.Model;

public class OrderItem
{
    public int Id { get; set; }
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
