using NUnit.Framework;
using Shouldly;
using Specify.Mocks;

namespace Specify.IntegrationTests.Mocks
{
    [TestFixture]
    public class FileSystemTests
    {
        [Test]
        public void should_return_true_if_assembly_available()
        {
            var sut = new FileSystem();
            sut.IsAssemblyAvailable("NSubstitute")
                .ShouldBe(true);
        }

        [Test]
        public void should_return_false_if_assembly_not_available()
        {
            var sut = new FileSystem();
            sut.IsAssemblyAvailable("blah")
                .ShouldBe(false);
        }
    }
}