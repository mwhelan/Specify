using System;
using System.Linq;
using FakeItEasy;
using Specify.Containers;
using Specify.Containers.Mocking;

namespace Specify.Examples.Mocks
{
    public class FakeItEasyMockFactory : IMockFactory
    {
        public object CreateMock(Type type)
        {
            var method = typeof(FakeItEasyMockFactory).GetMethods().Single(x => x.Name == "CreateMock" && x.GetGenericArguments().Length == 1);
            var genericMethod = method.MakeGenericMethod(type);

            return genericMethod.Invoke(this, new object[]{});
        }

        public bool IsAvailable()
        {
            return true;
        }

        public T CreateMock<T>() where T : class
        {
            return A.Fake<T>();
        }
    }
}