using System;
using Humanizer;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Specify
{
    [TestFixture]
    public abstract class Specification : ISpecification
    {
        [Test]
        public virtual void Run()
        {
            string title = BuildTitle();
            this.BDDfy(title, Category ?? string.Empty);
        }

        protected virtual string BuildTitle()
        {
            return Title ?? GetType().Name.Humanize(LetterCasing.Title);
        }

        // BDDfy methods
        public virtual void EstablishContext() { }
        public virtual void Setup() { }
        public virtual void TearDown() { }

        public virtual Type Story { get { return GetType(); } }
        public virtual string Title { get; set; }
        public string Category { get; set; }
    }
}
