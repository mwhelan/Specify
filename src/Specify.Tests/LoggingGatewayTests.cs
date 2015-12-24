using NUnit.Framework;
using Shouldly;
using Specify.Exceptions;
using Specify.Logging;

namespace Specify.Tests
{
    [TestFixture]
    public class LoggingGatewayTests
    {
        [Test]
        public void should_throw_LoggingException_for_invalid_log()
        {
            string sut = null;
            Should.Throw<LoggingException>(() => sut.Log().Info("something"))
                .Message.ShouldBe("Failed to log for  logger.");
        }
    }
}
