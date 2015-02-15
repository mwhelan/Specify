using System;

namespace Specify.Examples.Mocks
{
    public interface IMockFactory
    {
        object CreateMock(Type service);
    }
}