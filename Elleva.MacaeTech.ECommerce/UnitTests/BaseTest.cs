using AutoFixture;
using NUnit.Framework;

namespace Elleva.MacaeTech.ECommerce.UnitTests
{
    public class BaseTest
    {
        protected Fixture Fixture;
        
        [SetUp]
        public void BaseTestSetup()
        {
            Fixture = new Fixture();
        }
    }
}