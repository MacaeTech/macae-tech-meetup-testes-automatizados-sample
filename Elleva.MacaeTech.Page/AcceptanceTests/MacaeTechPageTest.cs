using System;
using Elleva.MacaeTech.AcceptanceTests.PageObject;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Elleva.MacaeTech.AcceptanceTests
{
    public class Tests
    {  
        private MacaeTechPage _page;
        private ChromeDriver _driver;
        

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(50);
            _driver.Url = "https://macae.tech/";

            _page = new MacaeTechPage(_driver);
        }

        [Test]
        public void clicar_em_reserve_sua_vaga_deve_ser_redirecionado_para_o_site_da_sympla()
        {
            // ARRANGE
            Assert.IsTrue(_page.BotaoReserveSuaVaga.Displayed);
            
            // ACT
            var symplaPage = _page.ClicarEmReserveSuaVaga();

            // ASSERT
            Assert.AreEqual("Macaé Tech Meetup #3 - Sympla", symplaPage.Titulo);
        }

        [Test]
        public void tentar_se_inscrever_sem_informar_o_email_deve_gerar_erro()
        {
            // ARRANGE
            var symplaPage = _page.ClicarEmReserveSuaVaga();
            symplaPage.IncrementarTotalDeIngressos();
            var formularioSympla = symplaPage.ClicarEmContinuar();
            
            // ACT
            formularioSympla.CampoNome.SendKeys("Phelipe");
            formularioSympla.CampoSobrenome.SendKeys("Perboires");
            formularioSympla.CampoProfissao.SendKeys("Desenvolvedor");
            formularioSympla.SelecionarCopiarDaInscricaoNumero1();
            
            formularioSympla.ClicarNoBotaoFinalizar();

            // ASSERT
            Assert.IsTrue(formularioSympla.ExisteTexto("O campo E-mail é obrigatório."));
            Assert.IsTrue(formularioSympla.ExisteTexto("O campo Confirmação do e-mail é obrigatório."));
            Assert.IsTrue(formularioSympla.ExisteTexto("E-mail não é um endereço de e-mail válido."));
        }
        
        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
        
        
        private static bool WaitUntilElementIsPresent(IWebDriver driver, By by, int timeout = 5)
        {
            for (var i = 0; i < timeout; i++)
            {
                if (driver.FindElement(by).Displayed) return true;
            }
            return false;
        }
    }
}