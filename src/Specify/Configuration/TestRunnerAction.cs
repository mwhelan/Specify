using System;

namespace Specify.Configuration
{
    public class TestRunnerAction : ITestRunnerAction
    {
        public Action BeforeAction { get; protected set; }
        public Action AfterAction { get; protected set; }

        internal TestRunnerAction()
        {
            BeforeAction = () => { };
            AfterAction = () => { };
        }

        public TestRunnerAction(Action startup, Action shutdown)
        {
            BeforeAction = startup;
            AfterAction = shutdown;
        }

        public void Before()
        {
            BeforeAction();
        }

        public void After()
        {
            AfterAction();
        }
    }
}