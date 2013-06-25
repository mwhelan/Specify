using NUnit.Framework;

namespace Specify.Tests
{
    [SetUpFixture]
    public class AssemblySetupFixture
    {
        [SetUp]
        public void SetUp()
        {
            SpecifyConfiguration.InitializeSpecify();
        }
    }
}
