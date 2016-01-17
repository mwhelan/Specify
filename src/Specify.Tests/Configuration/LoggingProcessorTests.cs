using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    [TestFixture]
    public class LoggingProcessorTests
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void LoggingOutputTest()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "nlog-sample.txt");
            using (new TemporaryNLogLogger(filePath))
            {
                var story = new ReportTestData()
                    .CreateOneStoryWithOneFailingScenarioAndOnePassingScenarioWithThreeStepsOfFiveMillisecondsAndEachHasTwoExamples();
                var sut = new ScenarioLoggingProcessor();
                sut.Process(story);

                Approvals.VerifyFile(filePath);
            }
        }

        //[Test]
        //[UseReporter(typeof(DiffReporter))]
        //public void LoggingConfigurationTest()
        //{
        //    string filePath = Path.Combine(Environment.CurrentDirectory, "nlog-sample-config.txt");
        //    using (new TemporaryNLogLogger(filePath))
        //    {
        //        var runner = new ScenarioRunner(new DefaultBootstrapper(), new DefaultApplicationContainer(), new BddfyTestEngine());
        //        runner.LogSpecifyConfiguration();
        //        Approvals.VerifyFile(filePath);
        //    }
        //}
    }
}
