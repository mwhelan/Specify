using System;
using Chill;

namespace Specify
{
    public abstract class SpecificationFor<TSubject>
        : GivenSubject<TSubject>, ISpecification
        where TSubject : class
    {
        public virtual Type Story
        {
            get { return typeof(TSubject); }
        }
        public virtual string Title { get { return GetType().Name.Humanize(LetterCasing.Title); } }

        public virtual void ExecuteTest()
        {
            TestRunner.Specify(this);
        }
    }

    public abstract class SpecificationFor<TSubject, TResult>
        : GivenSubject<TSubject, TResult>, ISpecification
        where TSubject : class
    {
        public virtual Type Story
        {
            get { return typeof (TSubject); }
        }

        public virtual string Title
        {
            get { return GetType().Name.Humanize(LetterCasing.Title); }
        }

        public virtual void ExecuteTest()
        {
            TestRunner.Specify(this);
        }
    }
}