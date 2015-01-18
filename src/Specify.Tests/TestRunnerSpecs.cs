//using System;
//using FluentAssertions;
//using NSubstitute;
//using NUnit.Framework;
//using Specify.Containers;
//using Specify.Tests.Stubs;

//namespace Specify.Tests
//{
//    [TestFixture]
//    public class TestRunnerSpecs
//    {
//        private ITestLifetimeScope _dependencyResolver = Substitute.For<ITestLifetimeScope>();

//        [Test]
//        public void should_throw_if_specification_is_null()
//        {
//            var sut = CreateSut();
//            Action result = () => sut.Run(null);
//            result.ShouldThrow<ArgumentNullException>()
//                .WithMessage("Value cannot be null.\r\nParameter name: testObject");
//        }

//        [Test]
//        public void should_run_each_specification_in_separate_container_scope()
//        {
//            var sut = CreateSut();
//            sut.Run(new ConcreteObjectWithNoConstructorSpecification());
//            sut.Container.Received().CreateTestLifetimeScope();
//        }

//        [Test]
//        public void should_resolve_specification_from_container_scope()
//        {
//            var sut = CreateSut();
//            sut.Run(new ConcreteObjectWithNoConstructorSpecification());
//            _dependencyResolver.Received().Resolve(typeof(ConcreteObjectWithNoConstructorSpecification));
//        }

//        [Test]
//        public void should_have_test_engine_execute_specification_with_custom_title()
//        {
//            var sut = CreateSut();
//            var specification = new ConcreteObjectWithNoConstructorSpecification();

//            sut.Run(specification);

//            sut.TestEngine.Received().Execute(Arg.Any<ConcreteObjectWithNoConstructorSpecification>(), specification.Title);
//        }

//        [Test]
//        public void should_dispose_container_lifetime_scope_after_each_test()
//        {
//            var sut = CreateSut();
//            var specification = new ConcreteObjectWithNoConstructorSpecification();
//            sut.Run(specification);

//            _dependencyResolver.Received().Dispose();
//        }

//        [Test]
//        public void should_dispose_container_after_all_tests()
//        {
//            var sut = CreateSut();
//            var specification = new ConcreteObjectWithNoConstructorSpecification();
//            sut.Run(specification);
                
//            sut.Dispose();

//            sut.Container.Received().Dispose();
//        }


//        private TestRunner CreateSut()
//        {
//            var sut = new TestRunner(Substitute.For<ITestContainer>(), Substitute.For<ITestEngine>());
//            _dependencyResolver
//                .Resolve(typeof(ConcreteObjectWithNoConstructorSpecification))
//                .Returns(new ConcreteObjectWithNoConstructorSpecification());
//            sut.Container.CreateTestLifetimeScope().Returns(_dependencyResolver);
//            return sut;
//        } 
//    }
//}
