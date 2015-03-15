using NSubstitute;
using NUnit.Framework;
using Specify.Configuration;
using TestStack.BDDfy;

namespace Specify.Tests.Configuration
{
    [TestFixture]
    public class BddfyTestEngineTests
    {
        [Test]
        public void should_handle_specification_with_examples()
        {
            var examples = new ExampleTable();
            var spec = Substitute.For<ISpecification>();
            spec.Examples.Returns(examples);
            var sut = new BddfyTestEngine();

            sut.Execute(spec);

            spec.Received().WithExamples(examples);
        }

        [Test]
        public void should_handle_specification_without_examples()
        {
            var spec = Substitute.For<ISpecification>();
            var sut = new BddfyTestEngine();

            sut.Execute(spec);

            spec.Received().BDDfy();
        }
    }
}
