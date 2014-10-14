using System;
using Specify.Containers;

namespace Specify.Configuration
{
    public class SpecifyConventions
    {
        public HtmlReportConfiguration HtmlReport { get; protected set; }

        public SpecifyConventions()
        {
            HtmlReport = new HtmlReportConfiguration();
        }

        public virtual void BeforeAllTests()
        {
        }

        public virtual void AfterAllTests()
        {
        }

        public virtual Func<IDependencyScope> DependencyFactory()
        {
            return () => new NSubstituteDependencyScope();
        } 
    }
}
