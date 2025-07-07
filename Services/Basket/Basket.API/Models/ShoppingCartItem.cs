namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!; // Assuming Color is a string, adjust as necessary
    public decimal Price { get; set; } = default!;

    public Guid ProductId { get; set; } = default!; // Assuming ProductId is a Guid, adjust as necessary

    public string ProductName { get; set; } = default!; // Assuming ProductName is a string, adjust as necessary
}
