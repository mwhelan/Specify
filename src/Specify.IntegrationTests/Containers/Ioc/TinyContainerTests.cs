using System.Linq;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class TinyContainerTests : IocContainerTestsFor<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var container = new TinyContainerFactory().Create(null);
            return new TinyContainer(container);
        }

        [Test]
        public void should_set_multiple_implementations_for_a_type_generic()
        {
            var sut = this.CreateSut();

            sut.SetMultiple<IDependency3>(new[] { typeof(Dependency3), typeof(Dependency4) });

            var result = sut.GetMultiple(typeof(IDependency3)).ToList();
            result.Count.ShouldBe(2);
            result.ForEach(x => x.ShouldBeAssignableTo<IDependency3>());
        }

        [Test]
        public void should_set_multiple_implementations_for_a_type_non_generic()
        {
            var sut = this.CreateSut();

            sut.SetMultiple(typeof(IDependency3), new[] { typeof(Dependency3), typeof(Dependency4) });

            var result = sut.Container.ResolveAll<IDependency3>().ToList();
            result.Count.ShouldBe(2);
            result.ForEach(x => x.ShouldBeAssignableTo<IDependency3>());
        }

        [Test]
        public void should_get_multiple_implementations_for_a_type_generic()
        {
            var sut = this.CreateSut();
            sut.Container.RegisterMultiple<IDependency3>(new[] { typeof(Dependency3), typeof(Dependency4) });

            var result = sut.GetMultiple<IDependency3>().ToList();
            result.Count.ShouldBe(2);
            result.ForEach(x => x.ShouldBeAssignableTo<IDependency3>());
        }

        [Test]
        public void should_get_multiple_implementations_for_a_type_non_generic()
        {
            var sut = this.CreateSut();
            sut.Container.RegisterMultiple<IDependency3>(new[] { typeof(Dependency3), typeof(Dependency4) });

            var result = sut.GetMultiple(typeof(IDependency3)).ToList();
            result.Count.ShouldBe(2);
            result.ForEach(x => x.ShouldBeAssignableTo<IDependency3>());
        }
    }
}