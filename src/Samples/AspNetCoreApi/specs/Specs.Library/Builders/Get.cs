using AutoFixture;
using Specs.Library.Builders.ObjectMothers;
using TestStack.Dossier;

namespace Specs.Library.Builders
{
    public static class Get
    {
        public static Builder<T> BuilderFor<T>() where T : class
        {
            return Builder<T>.CreateNew();
        }

        public static T InstanceOf<T>() where T : class
        {
            return Builder<T>.CreateNew().Build();
        }

        public static Stubs StubFor { get; } = new Stubs();

        public static AnonymousValueFixture Any { get; } = new AnonymousValueFixture();

        public static T AutoFixtureValueFor<T>()
        {
            return new AnonymousValueFixture().Fixture.Create<T>();
        }

        public static SequentialMother SequenceOf { get; } = new SequentialMother();
    }
}