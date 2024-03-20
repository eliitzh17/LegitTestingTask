using System.Text.RegularExpressions;
using Microsoft.Playwright;
using PlaywrightTests.Contract;
using PlaywrightTests.Steps;

namespace PlaywrightTests.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LegitTests : TestBase
{
    [Test]
    public async Task LoginTest()
    {
        //Setup 
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await InitBrowser(playwright);
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await LoginAndRegistrationSteps.Login(page,
            new Credentials(Config["Username"], Config["Password"]));
        
        var order = new Order("", "Ramat Gan, Israel", new List<Product>()
        {
            new Product(1, 20, 4),
            new Product(5, 10.5, 2)
        });
        
        await ShoppingSteps.AddProductsToShoppingCart(page, order.Products);
        
        await ShoppingSteps.NavigateToShoppingCart(this, page);
        await ShoppingSteps.ValidateProductInShoppingCart(this, page, order.Products);
        order.OrderNumber = await CheckOutSteps.CheckoutFromShoppingCart(page, order.Address);

        await CheckOutSteps.NavigateToOrders(this, page);
        await CheckOutSteps.ValidateOrder(this, page, [order]);
    }
}