using System.Linq;
using AutoFixture;
using Elleva.MacaeTech.ECommerce.Shared;
using NSubstitute;
using NUnit.Framework;

namespace Elleva.MacaeTech.ECommerce.UnitTests
{
    /// <summary>
    /// Testes de unidade da classe Carrinho.
    /// </summary>
    public class CarrinhoTest : BaseTest
    {
        private ICalculatorDesconto _calculatorDesconto;
        private Carrinho _sut;

        [SetUp]
        public void Setup()
        {
            _calculatorDesconto = Substitute.For<ICalculatorDesconto>();
            _sut = new Carrinho(_calculatorDesconto);
        }

        /// <summary>
        /// Teste simples de unidade.
        /// </summary>
        [Test]
        public void ao_adicionar_produtos_entao_a_quantidade_desse_deve_ser_atualizada()
        {
            // Arrange / PREPARAÇÃO
            var tvLCD = new Produto("TV LCD", 1500);
            
            // Act / AÇÃO
            _sut.AdicionarItem(tvLCD, 2);
            
            // Assert / AFIRMAÇÃO
            Assert.AreEqual(2, _sut.QuantidadePorProduto(tvLCD));
        }

        [Test]
        public void ao_atualizar_a_quantidade_de_um_produto_entao_a_quantidade_apenas_desse_produto_deve_ser_atualizada()
        {
            // Arrange / PREPARAÇÃO
            var tvLCD = new Produto("TV LCD", 1500);
            _sut.AdicionarItem(tvLCD, 2);
            
            var smartphone = new Produto("IPHONE X", 2300);
            _sut.AdicionarItem(smartphone, 1);
            
            // Act / AÇÃO
            _sut.AtualizarQuantidade(tvLCD, 3);
            
            // Assert / AFIRMAÇÃO
            Assert.AreEqual(3, _sut.QuantidadePorProduto(tvLCD), "Quantidade de TVs deveria ter atualizado de 2 para 3.");
            Assert.AreEqual(1, _sut.QuantidadePorProduto(smartphone), "Quantida de iPhone deveria ter permanecido em 1.");
        }
        
        /// <summary>
        /// Teste que utiliza assert para verificar se uma exceção foi lançada.
        /// Além disso, este teste é um tipo de teste parametrizado.
        /// </summary>
        /// <param name="quantidade"></param>
        [TestCase(0)]
        [TestCase(-1)]
        public void nao_deve_ser_possivel_adicionar_uma_quantidade_de_itens_zero_ou_negativa(int quantidade)
        {
            // Arrange / PREPARAÇÃO
            var tvLCD = new Produto("TV LCD", 1500);

            // Act / AÇÃO
            TestDelegate act = () => _sut.AdicionarItem(tvLCD, quantidade);
           
            // Assert / AFIRMAÇÃO
            var erro = Assert.Catch<ECommerceException>(act);
            Assert.AreEqual("Quantidade de produtos deve ser maior que zero.", erro.Message);
        }

        /// <summary>
        /// Teste usando MOCK.
        /// </summary>
        [Test]
        public void deve_salvar_no_carrinho_o_preco_unitario_do_produto_considerando_o_desconto_calculado()
        {
            // Arrange / PREPARAÇÃO
            var tvLCD = new Produto("TV LCD", 1500);

            _sut.DefineCodigoCupomDesconto("MACAE_TECH");
            
            _calculatorDesconto.CalcularDesconto(tvLCD, 1, "MACAE_TECH").Returns(300);
            _calculatorDesconto.CalcularDesconto(tvLCD, 1, "CODING_DOJO").Returns(200);
            
            // Act / AÇÃO
            _sut.AdicionarItem(tvLCD, 1);
           
            // Assert / AFIRMAÇÃO
            Assert.AreEqual(1200, _sut.Total,
                "Deveria ter aplicado o desconto de R$ 300,00 no produto referente ao cupom MACAE_TECH.");
        }

        [Test]
        public void ao_remover_um_produto_os_demais_deve_continuar_existindo_no_carrinho()
        {
            // Arrange / PREPARAÇÃO
            var produtos = Fixture.CreateMany<Produto>(10).ToList();

            foreach (var produto in produtos)
            {
                _sut.AdicionarItem(produto, 1);
            }
            
            // Act / AÇÃO
            var produtoRemovido = produtos[0];
            
            _sut.RemoverItem(produtoRemovido);
            
            // Assert / AFIRMAÇÃO
            Assert.AreEqual(0, _sut.QuantidadePorProduto(produtoRemovido), $"Não deveria ter nenhum produto de nome '{produtoRemovido.Nome}'.");
            Assert.AreEqual(9, _sut.TotalProdutos);
        }
    }
}