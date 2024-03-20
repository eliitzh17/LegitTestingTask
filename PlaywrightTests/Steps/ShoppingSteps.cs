using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Contract;

namespace PlaywrightTests.Steps;

public class ShoppingSteps
{
    public static async Task NavigateToShoppingCart(PageTest test, IPage page)
    {
        await test.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Shopping Cart" })).ToBeVisibleAsync();
        await test.Expect(page.GetByRole(AriaRole.Navigation)).ToContainTextAsync("Shopping Cart");
        await page.GetByRole(AriaRole.Link, new() { Name = "Shopping Cart" }).ClickAsync();
    }

    public static async Task AddProductsToShoppingCart(IPage page, List<Product> products)
    {
        foreach (var product in products)
        {
            var locator = page.Locator("li").Filter(new LocatorFilterOptions { HasText = product.ProductName });
            await locator.Locator("select").SelectOptionAsync(product.ProductQuantity.ToString());
            await locator.GetByRole(AriaRole.Button).ClickAsync();
        }
    }

    public static async Task ValidateProductInShoppingCart(PageTest test, IPage page, List<Product> products)
    {
        foreach (var product in products)
        {
            var productElement = page.GetByText(product.ProductName);
            await test.Expect(productElement).ToBeVisibleAsync();
            await test.Expect(productElement).ToContainTextAsync(product.ProductName);
            await test.Expect(productElement).ToContainTextAsync("$" + product.ProductPrice);
            await test.Expect(productElement).ToContainTextAsync("Quantity: " + product.ProductQuantity);
        }
    }
}