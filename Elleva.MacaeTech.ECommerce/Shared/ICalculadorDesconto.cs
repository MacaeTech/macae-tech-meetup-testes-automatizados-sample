namespace Elleva.MacaeTech.ECommerce.Shared
{
    public interface ICalculatorDesconto
    {
        decimal CalcularDesconto(Produto produto, int quantidade, string codigoCupom);
    }
}