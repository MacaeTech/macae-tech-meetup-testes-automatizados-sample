using System;

namespace Elleva.MacaeTech.ECommerce.Shared
{
    public class ECommerceException : Exception
    {
        public ECommerceException(string mensagem) : base(mensagem)
        {
            
        }
    }
}