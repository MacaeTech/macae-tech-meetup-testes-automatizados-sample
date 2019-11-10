using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Elleva.MacaeTech.AcceptanceTests.PageObject
{
    public class SymplaPage
    {
        private readonly IWebDriver _webDriver;

        public SymplaPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string Titulo => _webDriver.Title;

        public IWebElement BotaoIncrementarTotalDeIngressos =>
            _webDriver.FindElement(By.CssSelector(".icon-plus-circle"));


        public IWebElement BotaoContinuar => _webDriver.FindElement(By.Id("btnContinue"));

        public void IncrementarTotalDeIngressos()
        {
            BotaoIncrementarTotalDeIngressos.Click();
        }

        public SymplaFormularioInscricaoPage ClicarEmContinuar()
        {
            BotaoContinuar.Click();
            return new SymplaFormularioInscricaoPage(_webDriver);
        }
    }
}