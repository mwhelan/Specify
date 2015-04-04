using System;
using System.Reflection;

namespace Specify.Containers
{
    public class NSubstituteMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        public NSubstituteMockFactory()
        {
            var nsubAssembly = Assembly.Load("NSubstitute");
            _mockOpenType = nsubAssembly.GetType("NSubstitute.Substitute");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type NSubstitute.Substitute in assembly " + nsubAssembly.Location);
        }

        public object CreateMock(Type type)
        {
            var methods = _mockOpenType.GetMethods();
            foreach (var method in methods)
            {
                if (method.ContainsGenericParameters && method.GetGenericArguments().Length == 1)
                {
                    var generic = method.MakeGenericMethod(type);
                    if (generic == null)
                        throw new InvalidOperationException(
                            "Unable to find method For<>() on NSubstitute.Substitute.");
                    return generic.Invoke(null, new object[1] { null });
                }
            }
            return null;
        }

    }
}
