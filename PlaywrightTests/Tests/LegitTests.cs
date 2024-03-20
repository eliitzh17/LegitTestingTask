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
    public async Task FullLifecycleLegit()
    {
        //Prep
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await InitBrowser(playwright);
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync(Config["HomeAddress"]);
        await LoginAndRegistrationSteps.Login(page,
            new Credentials(Config["Username"], Config["Password"]));

        //Scenario start
        var order = new Order("", $"Bialik {new Random().Next(1, 1500)}, Israel, Ramat Gan", [
            new Product(1, 20, 5),
            new Product(5, 10.5, 2)
        ]);
        
        //Add to shopping cart
        await ShoppingSteps.AddProductsToShoppingCart(page, order.Products);
        await ShoppingSteps.NavigateToShoppingCart(this, page);
        await ShoppingSteps.ValidateProductInShoppingCart(this, page, order.Products);
        
        //Checkout
        order.OrderNumber = await CheckOutSteps.CheckoutFromShoppingCart(page, order.Address);
        await CheckOutSteps.NavigateToOrders(this, page);
        await CheckOutSteps.ValidateOrder(this, page, [order]);
    }

    [Test]
    public async Task RemoveOrderFromShoppingCart()
    {
        //Prep
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await InitBrowser(playwright);
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync(Config["HomeAddress"]);
        await LoginAndRegistrationSteps.Login(page,
            new Credentials(Config["Username"], Config["Password"]));

        //Scenario start
        var order = new Order("", $"Bialik {new Random().Next(1, 1500)}, Israel, Ramat Gan", [
            new Product(1, 20, 5),
            new Product(5, 10.5, 2)
        ]);
        
        //Add to shopping cart
        await ShoppingSteps.AddProductsToShoppingCart(page, order.Products);
        await ShoppingSteps.NavigateToShoppingCart(this, page);
        await ShoppingSteps.ValidateProductInShoppingCart(this, page, order.Products);
        
        await ShoppingSteps.RemoveProductFromShoppingCartAndValidate(this, page, [order.Products.ElementAt(0)]);
        order.Products.RemoveAt(0);
        
        //Checkout
        order.OrderNumber = await CheckOutSteps.CheckoutFromShoppingCart(page, order.Address);
        await CheckOutSteps.NavigateToOrders(this, page);
        await CheckOutSteps.ValidateOrder(this, page, [order]);
    }
}