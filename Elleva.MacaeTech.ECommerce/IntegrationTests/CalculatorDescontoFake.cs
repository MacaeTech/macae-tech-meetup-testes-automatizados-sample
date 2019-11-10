using Elleva.MacaeTech.ECommerce.Shared;

namespace Elleva.MacaeTech.ECommerce.IntegrationTests
{
    public class CalculatorDescontoFake : ICalculatorDesconto
    {
        public decimal CalcularDesconto(Produto produto, int quantidade, string codigoCupom)
        {
            return 0;
        }
    }
}