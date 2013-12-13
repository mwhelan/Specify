using NUnit.Framework;
using Specify.Configuration;

namespace Specify.Tests
{
    [SetUpFixture]
    public class AssemblySetupFixture
    {
        [SetUp]
        public void SetUp()
        {
            //SpecifyConfigurator.Initialize();
        }
    }
}
