namespace Elleva.MacaeTech.ECommerce.Shared
{
    public interface ICarrinho
    {
        void DefineCodigoCupomDesconto(string codigoCupom);
        void AdicionarItem(Produto produto, int quantidade);
        void RemoverItem(Produto produto);
        void AtualizarQuantidade(Produto produto, int novaQuantidade);
        
        decimal Total { get; }
        int TotalItens { get; }
        int QuantidadePorProduto(Produto produto);
        ItemCarrinho ObterItemPorProduto(Produto produto);
    }
}