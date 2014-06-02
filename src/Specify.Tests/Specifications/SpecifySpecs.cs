using NSubstitute;
using Shouldly;
using Specify.Core;
using Specify.Scanners;
using Xunit;

namespace Specify.Tests.Specifications
{
    public class when_creating_a_Specify_specification
    {
        [Fact]
        public void should_resolve_the_SUT()
        {
            var spec = new TestComponentSpecification();
            spec.SUT.ShouldBeAssignableTo<TestComponent>();
        }

        [Fact]
        public void should_resolve_the_Substitutes()
        {
            var spec = new TestComponentSpecification();
            spec.SUT.RunAll();
            spec.SubFor<IServiceA>().Received().RunA();
        }

        [Fact]
        public void should_set_the_correct_story_title()
        {
            var scanner = new SpecStoryMetaDataScanner();
            var metatdata = scanner.Scan(new TestComponentSpecification());
            metatdata.Title.ShouldBe("TestComponent");
        }

        #region stubs

        private class TestComponentSpecification : SpecificationFor<TestComponent>{}

        public interface IServiceA
        {
            void RunA();
        }

        public interface IServiceB
        {
            void RunB();
        }

        public class ServiceA : IServiceA
        {
            public void RunA() { }
        }

        public sealed class TestComponent
        {
            private readonly IServiceA _serviceA;
            private readonly IServiceB _serviceB;

            public TestComponent(IServiceA serviceA, IServiceB serviceB)
            {
                this._serviceA = serviceA;
                this._serviceB = serviceB;
            }

            public void RunAll()
            {
                this._serviceA.RunA();
                this._serviceB.RunB();
            }
        }
        #endregion

    }
}