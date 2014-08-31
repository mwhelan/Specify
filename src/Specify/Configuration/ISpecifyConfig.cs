using System;
using Specify.Containers;

namespace Specify.Configuration
{
    public interface ISpecifyConfig
    {
        void BeforeAllTests();
        void AfterAllTests();
        ReportConfig Report { get; set; }
        Func<IDependencyLifetime> GetChildContainer();
    }
}