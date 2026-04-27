namespace TestFramework.Models;

public class Order
{
    public string Id         { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public OrderStatus Status{ get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt{ get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
}

public class OrderItem
{
    public string Sku      { get; set; } = string.Empty;
    public int Quantity    { get; set; }
    public decimal UnitPrice { get; set; }
}

public enum OrderStatus { Pending, Processing, Shipped, Delivered, Cancelled }
