using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Contract;

namespace PlaywrightTests.Steps;

public class CheckOutSteps
{
    public static async Task NavigateToOrders(PageTest test, IPage page)
    {
        await test.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Orders" })).ToBeVisibleAsync();
        await test.Expect(page.GetByRole(AriaRole.Navigation)).ToContainTextAsync("Orders");
        await page.GetByRole(AriaRole.Link, new() { Name = "Orders" }).ClickAsync();
    }
    public static async Task<string> CheckoutFromShoppingCart(IPage page, string address)
    {
        await page.GetByRole(AriaRole.Button, new() { Name = "Proceed to Checkout" }).ClickAsync();
        await page.GetByLabel("Shipping Address:").FillAsync(address);

        var message = "";
        page.Dialog += (_, dialog) =>
        {
            message = dialog.Message;
            dialog.DismissAsync();
        };

        await page.GetByRole(AriaRole.Button, new() { Name = "Complete Checkout" }).ClickAsync();
        
        //Wait for dialog to disappear
        Thread.Sleep(1000);            

        return message.Split(": ")[1];
    }

    public static async Task ValidateOrder(PageTest test, IPage page, List<Order> orders)
    {
        foreach (var order in orders)
        {
            var existOrderLocator = page.GetByText(order.OrderNumber).Locator("..");
            await test.Expect(existOrderLocator).ToBeVisibleAsync();
            await test.Expect(existOrderLocator).ToContainTextAsync($"Order {order.OrderNumber} to {order.Address}");
            
            foreach (var product in order.Products)
            {
                var productElement = existOrderLocator.GetByText(product.ProductName);
                await test.Expect(productElement).ToBeVisibleAsync();
                await test.Expect(productElement).ToContainTextAsync(product.ProductName);
                await test.Expect(productElement).ToContainTextAsync("$" + product.ProductPrice);
                await test.Expect(productElement).ToContainTextAsync("Quantity: " + product.ProductQuantity);
            }
        }
    }
}