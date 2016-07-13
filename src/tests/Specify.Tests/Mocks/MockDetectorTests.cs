using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Mocks;

namespace Specify.Tests.Mocks
{
    [TestFixture]
    public class MockDetectorTests
    {
        [Test]
        public void should_return_NullMockFactory_if_no_mocks_available()
        {
            var sut = CreateSut(false, false, false);
            var result = sut.FindAvailableMock();
            result.ShouldBeOfType<NullMockFactory>();
        }

        [Test]
        public void should_return_Nsubstitute_if_available()
        {
            var sut = CreateSut(true, true, true);
            var result = sut.FindAvailableMock();
            result.ShouldBeOfType<NSubstituteMockFactory>();
        }

        [Test]
        public void should_return_FakeItEasy_if_available_and_nsubstitute_not()
        {
            var sut = CreateSut(false, true, true);
            var result = sut.FindAvailableMock();
            result.ShouldBeOfType<FakeItEasyMockFactory>();
        }

        [Test]
        public void should_return_Moq_if_available_and_nsubstitute_and_FakeItEasy_not()
        {
            var sut = CreateSut(false, false, true);
            var result = sut.FindAvailableMock();
            result.ShouldBeOfType<MoqMockFactory>();
        }

        [Test]
        public void should_return_true_if_assembly_available()
        {
            var sut = new MockDetector();
            var result = sut.FindAvailableMock();
            result.ShouldBeOfType<NSubstituteMockFactory>();
        }


        private MockDetector CreateSut(bool firstReturn, bool secondReturn, bool thirdReturn)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.IsAssemblyAvailable(Arg.Any<string>()).Returns(firstReturn, secondReturn, thirdReturn);
            var sut = new MockDetector(fileSystem);
            return sut;
        }
    }
}
