using Specify.Mocks;

namespace Specify.Tests.Mocks
{
    public class FakeItEasyMockFactoryTests : MockFactoryTestsFor<FakeItEasyMockFactory>
    {
        protected override string AssemblyName => "FakeItEasy";
        protected override string TypeName => "FakeItEasy.A";
        protected override IMockFactory CreateSut(IFileSystem fileSystem)
        {
            return new FakeItEasyMockFactory(fileSystem);
        }
    }
}