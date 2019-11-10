using System;
using Elleva.MacaeTech.ECommerce.Shared;
using NUnit.Framework;

namespace Elleva.MacaeTech.ECommerce.IntegrationTests
{
    public class CarrinhoRepositorioTests : BaseIntegrationTest
    {
        private CarrinhoRepositorio _sut;
        private string _databaseTeste;

        [SetUp]
        public void Setup()
        {
            _databaseTeste = $"teste_{Guid.NewGuid():N}";

            var connectionString = GenerateConnectionString(_databaseTeste);
           
            PrepararBanco(_databaseTeste);
            
            _sut = new CarrinhoRepositorio(connectionString);
        }

        [TearDown]
        public void TearDown()
        {
            DroparBanco(_databaseTeste);
        }

        [Test]
        public void testar_salvamento_e_carregamento_do_carrinho_e_seus_itens()
        {
            // Arrange / PREPARAÇÃO
            var calculatorDesconto = new CalculatorDescontoFake();
            var carrinho = new Carrinho(calculatorDesconto);
           
            var tvLCD = new Produto("TV LCD", 1500);
            carrinho.AdicionarItem(tvLCD, 2);
            
            var smartphone = new Produto("IPHONE X", 2300);
            carrinho.AdicionarItem(smartphone, 1);
            
            // Act / AÇÃO
            _sut.Salvar(carrinho);
            
            // Assert / AFIRMAÇÃO
            var carrinhoSalvo = _sut.Obter(carrinho.Codigo);
            
            Assert.AreEqual(carrinho.Codigo, carrinhoSalvo.Codigo);
            Assert.AreEqual(carrinho.CodigoCupom, carrinhoSalvo.CodigoCupom);

            var itemTvLCDSalvo = carrinho.ObterItemPorProduto(tvLCD);
            
            Assert.AreEqual(2, itemTvLCDSalvo.Quantidade);
            Assert.AreEqual(1500, itemTvLCDSalvo.ValorUnitario);
            Assert.AreEqual("TV LCD", itemTvLCDSalvo.NomeProduto);
            
            var itemSmartphoneSalvo = carrinho.ObterItemPorProduto(smartphone);
            
            Assert.AreEqual(1, itemSmartphoneSalvo.Quantidade);
            Assert.AreEqual(2300, itemSmartphoneSalvo.ValorUnitario);
            Assert.AreEqual("IPHONE X", itemSmartphoneSalvo.NomeProduto);
        }
        
    }
}