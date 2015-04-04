using System;
using System.Reflection;

namespace Specify.Containers
{
    public class FakeItEasyMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        public FakeItEasyMockFactory()
        {
            var assembly = Assembly.Load("FakeItEasy");
            _mockOpenType = assembly.GetType("FakeItEasy.A");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type FakeItEasy.A in assembly " + assembly.Location);
        }

        public object CreateMock(Type type)
        {
            var openFakeMethod = _mockOpenType.GetMethod("Fake", Type.EmptyTypes);
            var closedFakeMethod = openFakeMethod.MakeGenericMethod(type);

            try
            {
                return closedFakeMethod.Invoke(null, null);
            }
            catch
            {
                return null;
            }
        }
    }
}