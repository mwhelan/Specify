using System;

namespace Specify.Mocks
{
    /// <summary>
    /// Null mock factory adapter.
    /// </summary>
    public class NullMockFactory : IMockFactory
    {
        public string MockProviderName => "No mock factory detected";

        public object CreateMock(Type type)
        {
            throw new NotImplementedException();
        }

        public bool IsProviderAvailable => true;
    }
}
