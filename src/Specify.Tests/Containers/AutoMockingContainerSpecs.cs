using System;
using FluentAssertions;
using NUnit.Framework;
using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers
{
    public class AutoMockingContainerSpecs
    {
        [Test]
        public void should_create_sut_with_no_constructor()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest();
            result.Should().BeOfType<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_a_singleton()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var expected = sut.SystemUnderTest();

            var result = sut.SystemUnderTest();

            result.Should().BeSameAs(expected);
        }

        [Test]
        public void should_create_sut_with_greediest_constructor()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();

            var result = sut.SystemUnderTest();

            result.Should().BeOfType<ConcreteObjectWithMultipleConstructors>();
            result.Dependency1.Should().NotBeNull();
            result.Dependency2.Should().NotBeNull();
        }


        [Test]
        public void should_be_able_to_inject_dependencies_into_sut_before_sut_creation()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.InjectDependency<IDependency1>(new Dependency1());

            var result = sut.SystemUnderTest();

            result.Dependency1.Value.Should().Be(5);
        }

        [Test]
        public void should_not_be_able_to_inject_dependencies_after_sut_has_been_created()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            sut.SystemUnderTest();
            
            var result = Catch.Exception(() => sut.InjectDependency<IDependency1>(new Dependency1()));
            
            result.Should().BeOfType<InvalidOperationException>();
        }

        [Test]
        public void sut_dependency_should_be_a_singleton()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var expected = sut.DependencyFor<IDependency2>();

            var result = sut.DependencyFor<IDependency2>();

            result.Should().BeSameAs(expected);
        }

        [Test]
        public void non_sut_dependency_should_be_a_singleton()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var expected = sut.DependencyFor<IDependency3>();

            var result = sut.DependencyFor<IDependency3>();

            result.Should().BeSameAs(expected);
        }

        private static SpecificationContext<TSut> CreateSut<TSut>() where TSut : class
        {
            return new SpecificationContext<TSut>(new NSubstituteDependencyScope());
        }
    }
}
