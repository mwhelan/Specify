namespace Specify.Mocks
{
    /// <summary>
    /// An auto mocking container with NSubstitute, Moq or FakeItEasy.
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    public class AutoMockerFor<TSut> : ContainerFor<TSut>
        where TSut : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMockerFor{TSut}"/> class.
        /// </summary>
        public AutoMockerFor()
            : base(CreateContainer()) { }

        private static IContainer CreateContainer()
        {
            return Host.Configuration.ApplicationContainer.Get<IContainer>();
        }
    }
}