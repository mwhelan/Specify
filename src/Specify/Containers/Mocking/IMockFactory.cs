using System;

namespace Specify.Containers.Mocking
{
    public interface IMockFactory
    {
        object CreateMock(Type type);
    }
}