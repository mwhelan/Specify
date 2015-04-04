using System;

namespace Specify.Containers
{
    public interface IMockFactory
    {
        object CreateMock(Type type);
    }
}