using System;
using System.Reflection;

namespace Specify.Containers
{
    public class NSubstituteMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        public NSubstituteMockFactory()
        {
            var assembly = Assembly.Load("NSubstitute");
            _mockOpenType = assembly.GetType("NSubstitute.Substitute");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type NSubstitute.Substitute in assembly " + assembly.Location);
        }

        public object CreateMock(Type type)
        {
            var methods = _mockOpenType.GetMethods();
            foreach (var method in methods)
            {
                if (IsGenericForMethod(method))
                {
                    var closedGenericForMethod = method.MakeGenericMethod(type);
                    return closedGenericForMethod.Invoke(null, new object[1] { null });
                }
            }
            return null;
        }

        private static bool IsGenericForMethod(MethodInfo method)
        {
            return method.ContainsGenericParameters && method.GetGenericArguments().Length == 1;
        }
    }
}
