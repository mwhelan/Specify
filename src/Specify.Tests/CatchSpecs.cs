using System;

namespace Specify.Tests
{
    using NUnit.Framework;

    using Shouldly;

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
                .ShouldBe(null);
        }

        [Test]
        public void should_return_exception_from_action()
        {
            Catch.Exception(() => { throw new InvalidOperationException(); })
                .ShouldBeOfType<InvalidOperationException>();
        }
    }
}
