using OpenQA.Selenium;
using RpaInvesting.Utils;

namespace RpaInvesting.Constants
{
    class InvestingConstants
    {
        public static string INVESTING_URL = GetValue("url", "mainUrl");
        public static string EXCEL_INPUT_PATH = GetValue("files", "inputFile");       


        public static By byXPathSearchBox = By.XPath("//div/div/input");
        public static By byXPathClickAtivo = By.XPath("//div[contains(@class,'js-inner-all-results-quotes-wrapper')]//following-sibling::a");

        public static By byXPathAtivoName = By.XPath("//div/h1[contains(@class,'text-2xl')]");
        public static By byXPathAtivoPrice = By.XPath("//div/span[contains(@data-test,'instrument-price-last')]");
       

        public static string GetValue(string section, string key)
        {
            var iniFile = new IniFile(@"C:\Robos\rpa.investing\resources\config.ini");
            return iniFile.GetValue(section, key);
        }
    }
}
