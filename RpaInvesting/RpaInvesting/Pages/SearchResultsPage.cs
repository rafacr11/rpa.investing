using OpenQA.Selenium;
using RpaInvesting.Constants;
using RpaInvesting.Model;
using RpaInvesting.Utils;
using System;
using System.Globalization;
using System.Threading;

namespace RpaInvesting.Pages
{
    class SearchResultsPage
    {
        private readonly Logging log;                    
        public SearchResultsPage()
        {

        }
        public SearchResultsPage(Logging log)
        {            
            this.log = log;            
        }
       

        public bool isElementPresent(By by, IWebDriver chromeDriver)
        {
            try
            {
                chromeDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void searchAtivo(IWebDriver chromeDriver,Ativo ativo) {
            string searchUrl = "https://www.investing.com/search/?q=";
            chromeDriver.Navigate().GoToUrl(searchUrl + ativo.nomeAtivo);           

            if (isElementPresent(By.XPath("//div[contains(@class,'js-inner-all-results-quotes-wrapper')]//following-sibling::a"),chromeDriver) == true)
            {
                chromeDriver.FindElement(InvestingConstants.byXPathClickAtivo).Click();                
                readAtivoData(chromeDriver, ativo);
                log.Info("Dados do ativo  " + ativo.nomeAtivo + "  capturados");

            }
            else
            {
                chromeDriver.FindElement(InvestingConstants.byXPathSearchBox).Clear();       
                ativo.isProcessed = "Ativo não encontrado";                
                log.Info("Ativo  "+ ativo.nomeAtivo + "  não encontrado na busca");
            }
                                 
        }

        public Ativo readAtivoData(IWebDriver chromeDriver,Ativo ativo)
        {            
            DateTime thisDay = DateTime.Today;           

            ativo.nomeEmpresa = chromeDriver.FindElement(InvestingConstants.byXPathAtivoName).Text;           
            ativo.preco = Convert.ToDouble(chromeDriver.FindElement(InvestingConstants.byXPathAtivoPrice).Text, CultureInfo.InvariantCulture);            
            ativo.diaExecucao = thisDay.ToString("d");
            ativo.isProcessed = "Ok";
            return ativo;
        }
    }
}
