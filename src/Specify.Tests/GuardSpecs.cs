using System;
using FluentAssertions;
using NUnit.Framework;

namespace Specify.Tests
{
    public class GuardSpecs
    {
        [Test]
        public void should_throw_if_condition_true()
        {
            object nullObject = null;
            Action action = () => SpecifyExtensions.Specify(nullObject);
            action.ShouldThrow<ArgumentException>()
                .WithMessage("testObject cannot be null");
        }

        [Test]
        public void should_not_throw_if_condition_false()
        {
            object validObject = "something";
            Action action = () => SpecifyExtensions.Specify(validObject);
            action.ShouldNotThrow<ArgumentException>();
        }
    }
}
