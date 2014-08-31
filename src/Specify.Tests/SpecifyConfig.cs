using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Specify.Configuration;
using Specify.Containers;

namespace Specify.Tests
{
    class SpecifyConfig : ISpecifyConfig
    {
        public SpecifyConfig()
        {
            Report = new ReportConfig()
            {
                Header = "Specify",
                Description = "Unit Tests",
                Name = "SpecifySpecs.html"
            };
        }
        public void BeforeAllTests()
        {
            
        }

        public void AfterAllTests()
        {
        }

        public ReportConfig Report { get; set; }
        public Func<IDependencyLifetime> GetChildContainer()
        {
            return null;
        }
    }
}
