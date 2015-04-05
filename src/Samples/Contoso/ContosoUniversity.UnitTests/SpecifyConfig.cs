using Specify.Configuration;
using Specify.Containers;
using Specify.Containers.Mocking;

namespace ContosoUniversity.UnitTests
{
    public class SpecifyConfig : SpecifyConfiguration
    {
        public override IContainer GetSpecifyContainer()
        {
            return new AutoMockingContainer(new NSubstituteMockFactory());
        }
    }
}
