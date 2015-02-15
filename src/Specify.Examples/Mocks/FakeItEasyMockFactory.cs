using System;
using System.Linq;
using FakeItEasy;

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

        public T CreateMock<T>() where T : class
        {
            return A.Fake<T>();
        }
    }
}