using System;
using NSubstitute;

namespace Specify.Examples.Mocks
{
    public class NSubstituteMockFactory : IMockFactory
    {
        public object CreateMock(Type typedService)
        {
            return Substitute.For(new[] { typedService }, null);
        }
    }
}