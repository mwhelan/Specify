# Bootstrapper class
You configure Specify using a bootstrapper class that provides one convenient place for configuration. 

The bootstrapper class lets you configure the following items:

- **Html Report**: The configuration values for the BDDfy HTML report.
- **Logging**: A flag to specify whether logging of scenarios is enabled. If so, Specify adds a BDDfy processor to log the results of each scenario using LibLog.
- **Application Container**: Gets the IoC container that will be used for the lifetime of the application. This is the container that stores all of the scenarios you write and any other services you want to register. This would be the TinyIoc container for Specify, Autofac for the Autofac plugin, or you could write an adapter for a different IoC container. The bootstrapper class has a virtual ConfigureContainer method which you can override to register services in the container. You can also override the BuildApplicationContainer method, but you should only do that if you are using a different IoC container.
- **Mocking Framework**: Specify will automatically detect if you are using NSubstitute, Moq, or FakeItEasy (in that order). If you want to change which one Specify selects you are able to override the selection.
- **PerAppDomainActions**: Set actions that will be run before and after all the scenarios (at the beginning/end of AppDomain).
- **PerScenarioActions**: Set actions that will be run before and after each scenario.

## SpecifyBootstrapper.cs File
The `SpecifyBootstrapper.cs` file is injected into your test project when you install Specify. (You are free to rename it to anything you like. The important thing is that it inherits from the `IBootstrapSpecify` interface). The SpecifyBootstrapper.cs file actually inherits from the DefaultBootstrapper base class which takes care of wiring up the in-built TinyIoc container. You can add items to the TinyIoc container that you want to be available for every test in the `ConfigureContainer` method.

    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyBootstrapper : DefaultBootstrapper
    {
        public SpecifyBootstrapper()
        {
            LoggingEnabled = true;
            HtmlReport.ReportHeader = "Specify Examples";
            HtmlReport.ReportDescription = "Unit Specifications";
            HtmlReport.OutputPath = Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile("log-{Date}.txt")
                .CreateLogger();
        }

        /// <summary>
        /// Register any additional items into the TinyIoc container or leave it as it is. 
        /// </summary>
        /// <param name="container">The <see cref="TinyIoCContainer"/> container.</param>
        public override void ConfigureContainer(TinyIoCContainer container)
        {

        }
    }

If you use a Specify plugin, then that will have its own bootstrapper class, which you should use. For example, if you use Specify.Autofac, then it will inject a SpecifyAutofacBootstrapper.cs file for you to bootstrap the application with. The difference is that it will wire up the Autofac container and so the ConfigureContainer method will provide an Autofac `ContainerBuilder` parameter instead of the TinyIoc container.

    /// <summary>
    /// Register any additional items into the Autofac container using the ContainerBuilder or leave it as it is. 
    /// </summary>
    /// <param name="builder">The Autofac <see cref="ContainerBuilder"/>.</param>
    public override void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<SubcutaneousTestsAutofacModule>();
    }

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

## Mocking Framework
Say, for example, that you wanted to use Moq for mocking and that you were also using TestStack's test data library [Dossier](https://dossier.readme.io/), which has a dependency on NSubstitute. Specify would attempt to resolve requested services using NSubstitute, by convention, even though your specifications are using the Moq syntax. No worries, simply set the bootstrapper's `MockFactory` property to an instance of an object that implements IMockFactory. For example:

    MockFactory = new MoqMockFactory();

Specify has three built-in mock factory implementations, for NSubstitute, Moq, and FakeItEasy. If you want to plug in a different mocking framework, just implement this simple interface:

    public interface IMockFactory
    {
        object CreateMock(Type type);
    }

## Per Application Actions
Represent an action to be performed once per Application (before and after scenarios are run). Just create a class that implements the `IPerApplicationAction` interface. (This used to be called `IPerAppDomainAction` in earlier iterations of Specify, but with the advent of .Net Core this is less appropriate).

    public interface IPerApplicationAction
    {
        void Before();
        void After();
    }

For example, if you wanted to rebuild the database before all the tests ran:

    public class RebuildDatabaseAction : IPerApplicationAction
    {
        public void Before()
        {
            Db.RebuildDatabase();
        }

        public void After()
        {
            // do nothing
        }
    }

And then add an instance of the class in the constructor of the bootstrapper:

	PerAppDomainActions.Add(new RestDatabaseAction());
            
## Per Scenario Actions
Similar to the per Application action, but represents an action to be performed before and after each scenario. The  PerScenario action has access to the child container that is created for that particular scenario. Just create a class that implements the `IPerScenarioActions` interface.

    public interface IPerScenarioActions
    {
        void Before(IContainer container);
        void After();
    }

For example, if you wanted to reset the database data before each test (using Respawn):

    public class ResetDatabaseAction : IPerScenarioActions
    {
        public void Before(IContainer container)
        {
            var checkpoint = new Checkpoint
            {
                SchemasToExclude = new[] { "RoundhousE" },
                TablesToIgnore = new[] { "sysdiagrams", "__MigrationHistory" }
            };

            checkpoint.Reset(Db.ConnectionString);
        }

        public void After()
        {
            // do nothing
        }
    }


Again, you can add an instance of the action in the constructor of the bootstrapper.

	PerScenarioActions.Add(new ResetDatabaseAction());