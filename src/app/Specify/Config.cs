using System;
using TestStack.BDDfy.Configuration;

namespace Specify
{
    public static class Config
    {
        public static Func<IScenario, string> ScenarioTitle = scenario =>
        {
            var className = scenario.GetType().FullName
                .Replace(scenario.GetType().Namespace + ".", string.Empty);
            var title = Configurator.Scanners
                .Humanize(className)
                .ToTitleCase();
            if (scenario.Number != 0)
            {
                title = $"{scenario.Number:00}: {title}";
            }
            return title;
        };
    }
}
