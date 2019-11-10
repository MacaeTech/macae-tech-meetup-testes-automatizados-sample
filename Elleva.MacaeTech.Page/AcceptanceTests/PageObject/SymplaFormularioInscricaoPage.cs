using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Elleva.MacaeTech.AcceptanceTests.PageObject
{
    public class SymplaFormularioInscricaoPage
    {
        private readonly IWebDriver _webDriver;

        public SymplaFormularioInscricaoPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.ElementExists(By.Id("customFormField_firstName_0")));
        }

        public IWebElement CampoNome => _webDriver.FindElement(By.Id("customFormField_firstName_0"));
        public IWebElement CampoSobrenome => _webDriver.FindElement(By.Id("customFormField_lastName_0"));
        public IWebElement CampoEmail => _webDriver.FindElement(By.Id("customFormField_Email_0"));
        public IWebElement CampoProfissao => _webDriver.FindElement(By.Id("customFormField_903813_0"));
        public SelectElement ComboCopiarInformacoes => new SelectElement(_webDriver.FindElement(By.Id("ddlCopyFrom")));
        public IWebElement BotaoFinalizar => _webDriver.FindElement(By.Id("buttonFree"));

        public void ClicarNoBotaoFinalizar()
        {
            BotaoFinalizar.Click();
        }

        public void SelecionarCopiarDaInscricaoNumero1()
        {
            ComboCopiarInformacoes.SelectByText("Inscrição nº 1");
        }

        public bool ExisteTexto(string texto)
        {
            return _webDriver.PageSource.Contains(texto);
        }

    }
}