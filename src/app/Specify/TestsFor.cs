using Specify.Mocks;

namespace Specify
{
    /// <summary>
    /// A base fixture for unit testing which creates the System Under Test (SUT) and provides mocks with an auto mocking container.
    /// For those occasions when a test method per test is preferable to test class per scenario (ScenarioFor).
    /// This class will provide auto mocking functionality but does not utlise BDDfy and so these tests will not be included on BDDfy report.
    /// </summary>
    public abstract class TestsFor<TSut> where TSut : class
    {
        /// <summary>
        /// Sets up a new auto mocking container before each test.
        /// </summary>
        public void BaseSetup()
        {
            Container = new AutoMockerFor<TSut>();
        }

        /// <summary>
        /// Cleans up the SUT and Container after each test. Disposing the Auto Mocking Container will dispose its Autofac Container.
        /// </summary>
        public void BaseTearDown()
        {
            SUT = null;
            Container.Dispose();
        }

        /// <summary>
        /// An instance of an auto mocking container that exists for the lifetime of one test.
        /// </summary>
        protected ContainerFor<TSut> Container;

        private TSut _sut;

        /// <summary>
        /// The class that is being tested. Is lazily instantiated from the Container the first time it is called.
        /// This allows for adding particular constructor dependencies to the Container to be used in construction.
        /// The SUT can also be set directly if bypassing Container construction is required.
        /// </summary>
        protected TSut SUT
        {
            get { return _sut ?? (_sut = Container.Get<TSut>()); }
            set { _sut = value; }
        }
    }
}
