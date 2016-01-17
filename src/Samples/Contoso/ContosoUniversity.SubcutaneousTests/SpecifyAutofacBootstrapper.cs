using Autofac;
using Serilog;
using Specify.Autofac;

namespace ContosoUniversity.SubcutaneousTests
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyAutofacBootstrapper : DefaultAutofacBootstrapper
    {
        public SpecifyAutofacBootstrapper()
        {
            LoggingEnabled = true;
            var log = new LoggerConfiguration()
                .WriteTo.File("SerilogLog.txt",
                    outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                    .CreateLogger();
            Log.Logger = log;
        }

        /// <summary>
        /// Register any additional items into the Autofac container using the ContainerBuilder or leave it as it is. 
        /// </summary>
        /// <param name="builder">The Autofac <see cref="ContainerBuilder"/>.</param>
        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SubcutaneousTestsAutofacModule>();
        }
    }
}
