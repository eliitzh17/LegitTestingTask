namespace PlaywrightTests.Contract;

public class Order(string orderNumber, string address, List<Product> products)
{
    public string OrderNumber { get; set; } = orderNumber;
    public string Address { get; set; } = address;
    public List<Product> Products { get; set; } = products;
}