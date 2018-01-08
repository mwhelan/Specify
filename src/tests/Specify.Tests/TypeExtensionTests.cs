using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration.Scanners;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test]
        public void Create_generic_should_create_instance_of_type()
        {
            typeof(Dependency1).Create<IDependency1>()
                .ShouldBeOfType<Dependency1>();
        }

        [Test]
        public void Create_should_create_instance_of_type()
        {
            typeof(Dependency1).Create()
                .ShouldBeOfType<Dependency1>();
        }

        [Test]
        public void IsConcreteTypeOf_should_return_true_for_class()
        {
            typeof(Dependency1)
                .IsConcreteTypeOf<IDependency1>()
                .ShouldBeTrue();
            typeof(Dependency1)
                .IsConcreteTypeOf(typeof(IDependency1))
                .ShouldBeTrue();
        }

        [Test]
        public void IsConcreteTypeOf_should_return_false_for_abstract()
        {
            typeof(IDependency1)
                .IsConcreteTypeOf<IDependency1>()
                .ShouldBeFalse();
            typeof(ConfigScanner)
                .IsConcreteTypeOf<ConfigScanner>()
                .ShouldBeFalse();
            typeof(IDependency1)
                .IsConcreteTypeOf(typeof(IDependency1))
                .ShouldBeFalse();
            typeof(ConfigScanner)
                .IsConcreteTypeOf(typeof(ConfigScanner))
                .ShouldBeFalse();
        }

        [Test]
        public void CanBeCastTo_should_return_true_for_child_classes()
        {
            typeof(Dependency1)
                .CanBeCastTo<IDependency1>()
                .ShouldBeTrue();
            typeof(Dependency1)
                .CanBeCastTo(typeof(IDependency1))
                .ShouldBeTrue();
        }

        [Test]
        public void CanBeCastTo_should_return_false_for_nonchild_classes()
        {
            typeof(Dependency1)
                .CanBeCastTo<IDependency2>()
                .ShouldBeFalse();
            typeof(Dependency1)
                .CanBeCastTo(typeof(IDependency2))
                .ShouldBeFalse();
        }


        [Test]
        public void IsEnumerable_should_return_true_if_type_is_enumerable()
        {
            var array = new[] { "apples", "oranges", "pears" };
            var list = new List<int> { 1, 2, 3 };
            IList<string> ilist = new List<string> { "apples", "oranges", "pears" };
            IEnumerable<string> enumerable = Enumerable.Empty<string>();

            array.GetType().IsEnumerable().ShouldBe(true);
            list.GetType().IsEnumerable().ShouldBe(true);
            ilist.GetType().IsEnumerable().ShouldBe(true);
            enumerable.GetType().IsEnumerable().ShouldBe(true);
        }

        [TestCase(typeof(ConcreteObjectWithValueTypeConstuctorParmeter), true)]
        [TestCase(typeof(ConcreteObjectWithOneInterfaceConstructor), false)]
        [TestCase(typeof(ConcreteObjectWithOneConcreteConstructor), false)]
        public void HasAnyValueTypeParameters_should_return_true_if_any_constructor_parameters_are_value_types(Type type, bool result)
        {
            type.GreediestConstructor().HasAnyValueTypeParameters().ShouldBe(result);
        }


        //[Test]
        //public void GetTypeFromEnumerable_should_return_inner_type()
        //{
        //    var array = new[] { "apples", "oranges", "pears" };
        //    var list = new List<int> { 1, 2, 3 };
        //    IList<string> ilist = new List<string> { "apples", "oranges", "pears" };
        //    IEnumerable<string> enumerable = Enumerable.Empty<string>();

        //   // array.GetType().GetTypeFromEnumerable().ShouldBe(typeof(string));
        //    list.GetType().GetTypeFromEnumerable().ShouldBe(typeof(int));
        //    //ilist.GetType().GetTypeFromEnumerable().ShouldBe(typeof(string));
        //    //enumerable.GetType().GetTypeFromEnumerable().ShouldBe(typeof(string));
        //}

    }
}
