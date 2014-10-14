using System;
using FluentAssertions;
using NUnit.Framework;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    public class GuardSpecs
    {
        [Test]
        public void should_throw_if_condition_true()
        {
            ISpecification nullObject = null;
            Action action = () => SpecifyExtensions.Specify(nullObject);
            action.ShouldThrow<ArgumentException>()
                .WithMessage("testObject cannot be null");
        }

        [Test]
        public void should_not_throw_if_condition_false()
        {
            ISpecification validObject = new ConcreteObjectWithNoConstructorSpecification();
            Action action = () => SpecifyExtensions.Specify(validObject);
            action.ShouldNotThrow<ArgumentException>();
        }
    }
}
