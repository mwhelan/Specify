using Specify.Mocks;

namespace Specify.Tests.Mocks
{
    public class NSubstituteMockFactoryTests : MockFactoryTestsFor<NSubstituteMockFactory>
    {
        protected override string AssemblyName => "NSubstitute";
        protected override string TypeName => "NSubstitute.Substitute";
        protected override IMockFactory CreateSut(IFileSystem fileSystem)
        {
            return new NSubstituteMockFactory(fileSystem);
        }
    }
}