using Microsoft.Playwright;
using PlaywrightTests.Contract;

namespace PlaywrightTests;

public class LoginAndRegistrationSteps
{
    public static async Task Login(IPage page, Credentials credentials)
    {
        await page.GetByPlaceholder("Enter your Email").FillAsync(credentials.Username);
        await page.GetByPlaceholder("Enter your Password").FillAsync(credentials.Password);
        await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
    }
}