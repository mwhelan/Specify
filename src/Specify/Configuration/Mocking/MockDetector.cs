using System;
using System.Collections.Generic;
using System.Linq;
using Specify.Containers.Mocking;

namespace Specify.Configuration.Mocking
{
    public class MockDetector
    {
        private readonly IFileSystem _fileSystem;

        private readonly List<Tuple<string, Func<IMockFactory>>> _mockProviders = new List<Tuple<string, Func<IMockFactory>>>
        {
            new Tuple<string, Func<IMockFactory>>("NSubstitute", () => new NSubstituteMockFactory()),
            new Tuple<string, Func<IMockFactory>>("FakeItEasy", () => new FakeItEasyMockFactory()),
            new Tuple<string, Func<IMockFactory>>("Moq", () => new MoqMockFactory())
        };

        public MockDetector() 
            : this(new FileSystem()) { }

        public MockDetector(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Func<IMockFactory> FindAvailableMock()
        {
            return _mockProviders
                .Where(mock => _fileSystem.IsAssemblyAvailable(mock.Item1))
                .Select(mock => mock.Item2)
                .FirstOrDefault();
        }
    }
}