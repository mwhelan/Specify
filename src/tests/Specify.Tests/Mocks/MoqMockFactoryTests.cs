using Specify.Mocks;

namespace Specify.Tests.Mocks
{
    public class MoqMockFactoryTests : MockFactoryTestsFor<MoqMockFactory>
    {
        protected override string AssemblyName => "Moq";
        protected override string TypeName => "Moq.Mock`1";
        protected override IMockFactory CreateSut(IFileSystem fileSystem)
        {
            return new MoqMockFactory(fileSystem);
        }
    }
}