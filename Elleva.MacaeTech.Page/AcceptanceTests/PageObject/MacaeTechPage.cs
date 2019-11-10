using OpenQA.Selenium;

namespace Elleva.MacaeTech.AcceptanceTests.PageObject
{
    public class MacaeTechPage
    {
        private readonly IWebDriver _webDriver;

        public MacaeTechPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement BotaoReserveSuaVaga => _webDriver.FindElement(By.LinkText("Reserve sua vaga!"));

        public IWebElement ItemMenuOEvento => _webDriver.FindElement(By.LinkText("O evento"));

        public IWebElement ItemMenuPalestras => _webDriver.FindElement(By.LinkText("Palestras"));

        public IWebElement ItemMenuLocal => _webDriver.FindElement(By.LinkText("Local"));


        public SymplaPage ClicarEmReserveSuaVaga()
        {
            BotaoReserveSuaVaga.Click();
            
            return new SymplaPage(_webDriver);
        }
    }
}