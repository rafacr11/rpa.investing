using OpenQA.Selenium.Chrome;

namespace RpaInvesting.Browser
{
    class BrowserConfig
    {
        public static ChromeOptions GetChromeOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");                  
            options.AddUserProfilePreference("profile.managed_default_content_settings.javascript", 2);   

            return options;
        }
    }
}
