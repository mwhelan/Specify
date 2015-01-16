using System;
using Specify.Providers;

namespace Specify
{
    public abstract class SpecificationFor<TSubject> : ISpecification
        where TSubject : class
    {
        private ISpecifyContainer _container = new AutofacNSubstituteContainer();

        public TSubject Subject { get; set; }

        public T The<T>() where T : class
        {
            return _container.Get<T>();
        }

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
}