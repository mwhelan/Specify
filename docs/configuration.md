# Bootstrapper class
You configure Specify using a bootstrapper class that provides one convenient place for configuration. 

The bootstrapper class lets you configure the following items:

- **Html Report**: The configuration values for the BDDfy HTML report.
- **Logging**: A flag to specify whether logging of scenarios is enabled. If so, Specify adds a BDDfy processor to log the results of each scenario using LibLog.
- **Application Container**: Gets the IoC container that will be used for the lifetime of the application. This is the container that stores all of the scenarios you write and any other services you want to register. This would be the TinyIoc container for Specify, Autofac for the Autofac plugin, or you could write an adapter for a different IoC container. The bootstrapper class has a virtual ConfigureContainer method which you can override to register services in the container. You can also override the BuildApplicationContainer method, but you should only do that if you are using a different IoC container.
- **PerAppDomainActions**: Set actions that will be run before and after all the scenarios (at the beginning/end of AppDomain).
- **PerScenarioActions**: Set actions that will be run before and after each scenario.

## SpecifyBootstrapper.cs File
The `SpecifyBootstrapper.cs` file is injected into your test project when you install Specify. (You are free to rename it to anything you like. The important thing is that it inherits from the `IBootstrapSpecify` interface). The SpecifyBootstrapper.cs file actually inherits from the DefaultBootstrapper base class which takes care of wiring up the in-built TinyIoc container.

## Html Report
You can configure the report in the bootstrapper constructor using the HtmlReport property.

    public class SpecifyBootstrapper : DefaultBootstrapper
    {
        public SpecifyBootstrapper()
        {
            HtmlReport.ReportHeader = "Contoso University";
            HtmlReport.ReportDescription = "Unit Specifications";
            HtmlReport.ReportType = HtmlReportConfiguration.HtmlReportType.Metro;
            HtmlReport.OutputFileName = "metro.html";
        }
		...
    }

This extends the BDDfy `DefaultHtmlReportConfiguration` class, so you can configure everything on the report that BDDfy lets you configure. In addition, you can set the `ReportType` to `Classic` or `Metro`.

## Per AppDomain Actions
Represent an action to be performed per AppDomain (before and after scenarios are run). Just create a class that implements the `IPerAppDomainActions` interface.

    public interface IPerAppDomainActions
    {
        void Before();
        void After();
    }

And then add an instance of the class in the constructor of the bootstrapper:

	PerAppDomainActions.Add(new MyAppDomainAction());
            
## Per Scenario Actions
Similar to the per AppDomain action, but represents an action to be performed before and after each scenario. The  PerScenario action has access to the child container that is created for that particular scenario. Just create a class that implements the `IPerScenarioActions` interface.

    public interface IPerScenarioActions
    {
        void Before(IContainer container);
        void After();
    }

Again, you can add an instance of the action in the constructor of the bootstrapper.

	PerScenarioActions.Add(new MyScenarioAction());