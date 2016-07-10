using System.Collections.Generic;
using System.Linq;

namespace Specify.Mocks
{
    /// <summary>
    /// Detects whether one of the NSubstitute, FakeItEasy, or Moq mocking providers has been referenced by the test project.
    /// </summary>
    public class MockDetector
    {
        /// <summary>
        /// The collection of mock providers
        /// </summary>
        private readonly List<IMockFactory> _mockProviders;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDetector"/> class.
        /// </summary>
        public MockDetector() 
            : this(new FileSystem()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDetector"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public MockDetector(IFileSystem fileSystem)
        {
            _mockProviders = new List<IMockFactory>
            {
                new NSubstituteMockFactory(fileSystem),
                new FakeItEasyMockFactory(fileSystem),
                new MoqMockFactory(fileSystem),
                new NullMockFactory()
            };
        }

        /// <summary>
        /// Finds the first mock provider that is registered.
        /// </summary>
        /// <returns>The mock factory or null if none are registered.</returns>
        public IMockFactory FindAvailableMock()
        {
            return _mockProviders
                .FirstOrDefault(mock => mock.IsProviderAvailable);
        }
    }
}