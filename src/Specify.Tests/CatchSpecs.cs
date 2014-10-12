using System;
using FluentAssertions;
using NUnit.Framework;

namespace Specify.Tests
{
    public class CatchSpecs
    {
        [Test]
        public void should_return_null_if_no_exception()
        {
            Catch.Exception(() =>
            {
                var x = 1;
                x++;
            })
                .Should().Be(null);
        }

        [Test]
        public void should_return_exception_from_action()
        {
            Catch.Exception(() => { throw new InvalidOperationException(); })
                .Should().BeOfType<InvalidOperationException>()
                .And.Should().NotBeNull();
        }
    }
}
