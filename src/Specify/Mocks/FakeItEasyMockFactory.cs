using System;

namespace Specify.Mocks
{
    public class FakeItEasyMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        public FakeItEasyMockFactory() 
            : this(new FileSystem()) { }

        public FakeItEasyMockFactory(IFileSystem fileSystem)
        {
            var assembly = fileSystem.Load("FakeItEasy");
            _mockOpenType = fileSystem.GetTypeFrom(assembly, "FakeItEasy.A");
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