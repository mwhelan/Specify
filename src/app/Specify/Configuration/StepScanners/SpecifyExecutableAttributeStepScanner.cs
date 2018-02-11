using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify.Configuration.StepScanners
{
    public class SpecifyExecutableAttributeStepScanner : IStepScanner
    {
        public IEnumerable<Step> Scan(ITestContext testContext, MethodInfo candidateMethod)
        {
            var executableAttribute = (ExecutableAttribute)candidateMethod.GetCustomAttributes(typeof(ExecutableAttribute), true).FirstOrDefault();
            if (executableAttribute == null)
                yield break;

            var stepTitle = new StepTitle(executableAttribute.StepTitle);
            if (string.IsNullOrEmpty(stepTitle))
                stepTitle = new StepTitle(Configurator.Scanners.Humanize(candidateMethod.Name));

            var stepAsserts = IsAssertingByAttribute(candidateMethod);
            var shouldReport = executableAttribute.ShouldReport;

            var runStepWithArgsAttributes = (RunStepWithArgsAttribute[])candidateMethod.GetCustomAttributes(typeof(RunStepWithArgsAttribute), true);
            if (runStepWithArgsAttributes.Length == 0)
            {
                var stepAction = StepActionFactory.GetStepAction(candidateMethod, new object[0]);
                yield return new Step(stepAction, stepTitle, stepAsserts, executableAttribute.ExecutionOrder, shouldReport, new List<StepArgument>())
                {
                    ExecutionSubOrder = executableAttribute.Order
                };
            }

            foreach (var runStepWithArgsAttribute in runStepWithArgsAttributes)
            {
                var inputArguments = runStepWithArgsAttribute.InputArguments;
                var flatInput = inputArguments.FlattenArrays();
                var stringFlatInputs = flatInput.Select(i => i.ToString()).ToArray();
                var methodName = stepTitle + " " + string.Join(", ", stringFlatInputs);

                if (!string.IsNullOrEmpty(runStepWithArgsAttribute.StepTextTemplate))
                    methodName = string.Format(runStepWithArgsAttribute.StepTextTemplate, flatInput);
                else if (!string.IsNullOrEmpty(executableAttribute.StepTitle))
                    methodName = string.Format(executableAttribute.StepTitle, flatInput);

                var stepAction = StepActionFactory.GetStepAction(candidateMethod, inputArguments);
                yield return new Step(stepAction, new StepTitle(methodName), stepAsserts,
                                      executableAttribute.ExecutionOrder, shouldReport, new List<StepArgument>())
                {
                    // TODO: This needs to be fixed in BDDfy and this class can be removed from Specify
                    ExecutionSubOrder = executableAttribute.Order
                };
            }
        }

        public IEnumerable<Step> Scan(ITestContext testContext, MethodInfo method, Example example)
        {
            var executableAttribute = (ExecutableAttribute)method.GetCustomAttributes(typeof(ExecutableAttribute), true).FirstOrDefault();
            if (executableAttribute == null)
                yield break;

            string stepTitle = executableAttribute.StepTitle;
            if (string.IsNullOrEmpty(stepTitle))
                stepTitle = Configurator.Scanners.Humanize(method.Name);

            var stepAsserts = IsAssertingByAttribute(method);
            var shouldReport = executableAttribute.ShouldReport;
            var methodParameters = method.GetParameters();

            var inputs = new List<object>();
            var inputPlaceholders = Regex.Matches(stepTitle, " <(\\w+)> ");

            for (int i = 0; i < inputPlaceholders.Count; i++)
            {
                var placeholder = inputPlaceholders[i].Groups[1].Value;

                for (int j = 0; j < example.Headers.Length; j++)
                {
                    if (example.Values.ElementAt(j).MatchesName(placeholder))
                    {
                        inputs.Add(example.GetValueOf(j, methodParameters[inputs.Count].ParameterType));
                        break;
                    }
                }
            }

            var stepAction = StepActionFactory.GetStepAction(method, inputs.ToArray());
            yield return new Step(stepAction, new StepTitle(stepTitle), stepAsserts, executableAttribute.ExecutionOrder, shouldReport, new List<StepArgument>())
            {
                ExecutionSubOrder = executableAttribute.Order
            };
        }

        private static bool IsAssertingByAttribute(MethodInfo method)
        {
            var attribute = GetExecutableAttribute(method);
            return attribute.Asserts;
        }

        private static ExecutableAttribute GetExecutableAttribute(MethodInfo method)
        {
            return (ExecutableAttribute)method.GetCustomAttributes(typeof(ExecutableAttribute), true).First();
        }
    }
}
