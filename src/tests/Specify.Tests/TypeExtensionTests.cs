using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    [TestFixture]
    public class TypeExtensionTests
    {
        [Test]
        public void UnitScenario_instance_should_return_true_if_UnitScenario()
        {
            new StubUnitScenario()
                .IsUnitScenario()
                .ShouldBe(true);
        }

        [Test]
        public void UnitScenario_type_should_return_true_if_UnitScenario()
        {
            typeof(StubUnitScenario)
                .IsUnitScenario()
                .ShouldBe(true);
        }

        [Test]
        public void StoryScenario_instance_should_return_false_if_not_UnitScenario()
        {
            new StubUserStoryScenario()
                .IsUnitScenario()
                .ShouldBe(false);;
        }

        [Test]
        public void StoryScenario_type_should_return_false_if_not_UnitScenario()
        {
            typeof (StubUserStoryScenario)
                .IsUnitScenario()
                .ShouldBe(false);
        }

        [Test]
        public void NonScenario_type_should_return_false_if_not_UnitScenario()
        {
            typeof(Dependency1)
                .IsUnitScenario()
                .ShouldBe(false);
        }

        [Test]
        public void StoryScenario_instance_should_return_true_if_StoryScenario()
        {
            new StubUserStoryScenario()
                .IsStoryScenario()
                .ShouldBe(true);
        }

        [Test]
        public void StoryScenario_type_should_return_true_if_StoryScenario()
        {
            new StubUserStoryScenario()
                .IsStoryScenario()
                .ShouldBe(true);
        }

        [Test]
        public void StoryScenario_instance_should_return_false_if_not_StoryScenario()
        {
            new StubUnitScenario()
                .IsStoryScenario()
                .ShouldBe(false);;
        }

        [Test]
        public void StoryScenario_type_should_return_false_if__not_StoryScenario()
        {
            typeof (StubUnitScenario)
                .IsStoryScenario()
                .ShouldBe(false);
        }

        [Test]
        public void NonScenario_type_should_return_false_if_not_StoryScenario()
        {
            typeof(Dependency1)
                .IsStoryScenario()
                .ShouldBe(false);
        }

        [Test]
        public void Nested_StoryScenario_instance_should_return_true_if_StoryScenario()
        {
            new UserStoryScenarioWithAllSupportedStepsInRandomOrder()
                .IsStoryScenario()
                .ShouldBe(true); ;
        }

        [Test]
        public void Nested_StoryScenario_type_should_return_true_if_StoryScenario()
        {
            typeof(UserStoryScenarioWithAllSupportedStepsInRandomOrder)
                .IsStoryScenario()
                .ShouldBe(true); ;
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
