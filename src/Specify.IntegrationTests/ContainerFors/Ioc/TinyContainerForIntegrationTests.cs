using System.Linq;
using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.ContainerFors.Ioc
{
    public class TinyContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = new TinyContainer();
            container.Set<IDependency1, Dependency1>();
            container.Set<IDependency2, Dependency2>();
            container.SetMultiple<IDependency3>(new []{typeof(Dependency3), typeof(Dependency4)});
            container.Set<ConcreteObjectWithNoConstructor>();
            container.Set<ConcreteObjectWithMultipleConstructors>();
            container.Set<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            return new ContainerFor<T>(container);
        }

        /// <summary>
        /// Temporarily here while I build out this feature for all containers.
        /// Will live in ContainerForIntegrationTestsBase once all Containers have this feature.
        /// </summary>
        [Test]
        public void SystemUnderTest_should_resolve_collection_in_constructor()
        {
            var sut = this.CreateSut<ConcreteObjectWithOneInterfaceCollectionConstructor>();
            var result = sut.SystemUnderTest;
            result.Collection.ToList().Count.ShouldBe(2);
        }
    }
}
