using System;
using TestStack.BDDfy;

namespace Specify.Core
{
    public abstract class Specification : ISpecification
    {
        public virtual void Run()
        {
            string title = BuildTitle();
            this.BDDfy(title);
        }

        protected virtual string BuildTitle()
        {
            return Title ?? GetType().Name.Humanize(LetterCasing.Title);
        }

        public virtual Type Story { get { return GetType(); } }
        public virtual string Title { get; set; }
        public string Category { get; set; }
    }
}
