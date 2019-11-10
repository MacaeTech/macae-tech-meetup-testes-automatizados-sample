namespace Elleva.MacaeTech.ECommerce.Shared
{
    public class Produto
    {
        public Produto(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
        }
        
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}