using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RpaInvesting.Browser;
using RpaInvesting.Model;
using RpaInvesting.Pages;
using RpaInvesting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpaInvesting.Controller
{
    class RpaController
    {
        private readonly Logging log;
        private static IWebDriver chromeDriver= new ChromeDriver(BrowserConfig.GetChromeOptions());
        private SearchResultsPage searchResultsPage;      
       

        public RpaController(Logging log)
        {
            this.log = log;
        }

        public void StartFlow()
        {
            ExcelController excelController = new ExcelController(log);
            List<Ativo> ativos = excelController.ReadExcel();
            



            if (ativos.Count != 0)
            {
                log.Info("Quantidade de ativos na planilha: " + ativos.Count());           

                foreach (Ativo ativo in ativos)
                {
                    InvestingNavigation(ativo);
                    log.Info("Ativo " + ativo.nomeAtivo + " processado");
                    Console.WriteLine("");
                }

                excelController.OutputExcel(ativos);

                chromeDriver.Close();
                chromeDriver.Quit();


            }
            else
            {
                log.Info("Sem ativos a serem processados");
            }


        }

        public void InvestingNavigation(Ativo ativo)
        {
            this.searchResultsPage = new SearchResultsPage(log);
            try
            {                          
                searchResultsPage.searchAtivo(chromeDriver, ativo);       

            } catch(Exception e)
            {
                log.Error("Erro ao ler dados da página: " + ativo.nomeAtivo + " Erro: " + e.Message);
            }
            
        }
    }
}
