using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Exceptions;
using PlaywrightTests.Steps;

namespace PlaywrightTests;

public class TestBase : PageTest
{
    protected static Dictionary<string, string> Config = null!;
    
    [OneTimeSetUp]
    public void Init()
    {
        Config = Utils.ReadPropertiesFile("ConfigFiles/ConfigFile.json");
    }

    protected async Task<IBrowser> InitBrowser(IPlaywright playwright)
    {
        IBrowser browser = Config["Browser"].ToLower() switch
        {
            "chrome" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                SlowMo = int.Parse(Config["SlowMotionMS"]), Headless = bool.Parse(Config["Headless"]),
            }),
            "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                SlowMo = int.Parse(Config["SlowMotionMS"]), Headless = bool.Parse(Config["Headless"]),
            }),
            _ => throw new LegitException(Config["Browser"] + " browser is not supported!")
        };

        return browser;
    }
}