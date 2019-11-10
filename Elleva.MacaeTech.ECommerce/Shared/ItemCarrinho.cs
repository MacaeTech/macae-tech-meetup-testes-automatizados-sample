using System;

namespace Elleva.MacaeTech.ECommerce.Shared
{
    public class ItemCarrinho
    {
        public ItemCarrinho()
        {
            
        }
        
        public ItemCarrinho(Produto produto, int quantidade, decimal valorUnitario)
        {
            Codigo = Guid.NewGuid();
            NomeProduto = produto.Nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid Codigo { get; private set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string NomeProduto { get; private set; }
    }
}