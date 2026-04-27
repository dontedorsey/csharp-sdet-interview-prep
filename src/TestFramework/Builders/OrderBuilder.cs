using TestFramework.Models;

namespace TestFramework.Builders;

public class OrderBuilder : BuilderBase<OrderBuilder, Order>
{
    public OrderBuilder()
    {
        Entity = new Order
        {
            Id         = Guid.NewGuid().ToString(),
            CustomerId = Guid.NewGuid().ToString(),
            Status     = OrderStatus.Pending,
            CreatedAt  = DateTime.UtcNow,
            Items      = new List<OrderItem>()
        };
    }

    public OrderBuilder WithCustomerId(string customerId) { Entity.CustomerId = customerId; return this; }
    public OrderBuilder WithStatus(OrderStatus status)    { Entity.Status     = status;     return this; }
    public OrderBuilder WithCreatedAt(DateTime date)      { Entity.CreatedAt  = date;       return this; }

    public OrderBuilder WithItem(string sku, int quantity, decimal unitPrice)
    {
        Entity.Items.Add(new OrderItem { Sku = sku, Quantity = quantity, UnitPrice = unitPrice });
        return this;
    }
}
