using System;

namespace Specify.Mocks
{
    /// <summary>
    /// The contract for the different mock providers to implement to create a mock object.
    /// </summary>
    public interface IMockFactory
    {
        /// <summary>
        /// The name of the mocking framework that the factory uses.
        /// </summary>
        /// <returns>Mocking framework name</returns>
        string MockProviderName { get; }

        /// <summary>
        /// Creates the mock.
        /// </summary>
        /// <param name="type">The type to be mocked.</param>
        /// <returns>System.Object.</returns>
        object CreateMock(Type type);
    }
}