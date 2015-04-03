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
        public void thetest()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "nlog-sample.txt");
            using (new TemporaryNLogLogger(filePath))
            {
                var story = new ReportTestData()
                    .CreateOneStoryWithOneFailingScenarioAndOnePassingScenarioWithThreeStepsOfFiveMillisecondsAndEachHasTwoExamples();
                var sut = new LoggingProcessor();
                sut.Process(story);

                Approvals.VerifyFile(filePath);
            }
        }
    }
}
