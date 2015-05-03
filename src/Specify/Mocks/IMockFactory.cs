using System;

namespace Specify.Mocks
{
    public interface IMockFactory
    {
        object CreateMock(Type type);
    }
}