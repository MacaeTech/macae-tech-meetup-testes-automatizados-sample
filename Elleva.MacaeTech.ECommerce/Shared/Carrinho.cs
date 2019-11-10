using System;
using System.Collections.Generic;
using System.Linq;

namespace Elleva.MacaeTech.ECommerce.Shared
{
    
    public class Carrinho : ICarrinho
    {
        private readonly ICalculatorDesconto _calculatorDesconto;
        private IList<ItemCarrinho> _itens = new List<ItemCarrinho>();
        
        public string CodigoCupom { get; private set; }
        public Guid Codigo { get; private set; }

        public Carrinho()
        {
            
        }
        
        public Carrinho(ICalculatorDesconto calculatorDesconto, Guid? codigo = null)
        {
            _calculatorDesconto = calculatorDesconto;
            Codigo = codigo??Guid.NewGuid();
        }

        public void AdicionarItem(Produto produto, int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ECommerceException("Quantidade de produtos deve ser maior que zero.");
            }

            var desconto = _calculatorDesconto.CalcularDesconto(produto, quantidade, CodigoCupom);

            var valorUnitario = produto.Valor - desconto;
            
            _itens.Add(new ItemCarrinho(produto, quantidade, valorUnitario));
        }

        public void DefineCodigoCupomDesconto(string codigoCupom)
        {
            CodigoCupom = codigoCupom;
        }

        public void RemoverItem(Produto produto)
        {
            var item = _itens.FirstOrDefault(x => x.NomeProduto == produto.Nome);

            if (item != null)
            {
                _itens.Remove(item);
            }
        }

        public void AtualizarQuantidade(Produto produto, int novaQuantidade)
        {
            var item = _itens.FirstOrDefault(x => x.NomeProduto == produto.Nome);

            if (item == null)
            {
                throw new ECommerceException("Não existe item no carrinho relacionado a este produto.");
            }
                
            item.Quantidade = novaQuantidade;
        }

        public decimal Total => _itens.Sum(x => x.Quantidade * x.ValorUnitario);

        public int TotalItens => _itens.Count;

        public int TotalProdutos => _itens.Sum(x => x.Quantidade);

        public int QuantidadePorProduto(Produto produto)
        {
            var item = _itens.FirstOrDefault(x => x.NomeProduto == produto.Nome);

            if (item == null)
            {
                return 0;
            }

            return item.Quantidade;
        }

        public ItemCarrinho ObterItemPorProduto(Produto produto)
        {
            return _itens.FirstOrDefault(x => x.NomeProduto == produto.Nome);
        }

        internal IList<ItemCarrinho> Itens
        {
            get => _itens;
            set => _itens = value;
        }
    }
}