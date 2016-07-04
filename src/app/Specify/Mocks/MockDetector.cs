using System;
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
        /// A simple abstraction of the file system.
        /// </summary>
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// The collection of mock providers
        /// </summary>
        private readonly List<Tuple<string, Func<IMockFactory>>> _mockProviders = new List<Tuple<string, Func<IMockFactory>>>
        {
            new Tuple<string, Func<IMockFactory>>("NSubstitute", () => new NSubstituteMockFactory()),
            new Tuple<string, Func<IMockFactory>>("FakeItEasy", () => new FakeItEasyMockFactory()),
            new Tuple<string, Func<IMockFactory>>("Moq", () => new MoqMockFactory())
        };

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
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Finds the first mock provider that is registered.
        /// </summary>
        /// <returns>The mock factory or null if none are registered.</returns>
        public Func<IMockFactory> FindAvailableMock()
        {
            return _mockProviders
                .Where(mock => _fileSystem.IsAssemblyAvailable(mock.Item1))
                .Select(mock => mock.Item2)
                .FirstOrDefault();
        }
    }
}