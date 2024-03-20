namespace PlaywrightTests.Contract;

public class Product(int productId, double productPrice, int productQuantity)
{
    public string ProductName { get; set; } = "Product " + productId;
    public int ProductId { get; set; } = productId;
    public double ProductPrice { get; set; } = productPrice;
    public int ProductQuantity { get; set; } = productQuantity;
}