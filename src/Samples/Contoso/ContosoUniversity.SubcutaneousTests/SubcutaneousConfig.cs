using Autofac;
using Serilog;
using Specify.Autofac;

namespace ContosoUniversity.SubcutaneousTests
{
    public class SubcutaneousConfig : SpecifyAutofacBootstrapper
    {
        public SubcutaneousConfig()
        {
            LoggingEnabled = true;
            var log = new LoggerConfiguration()
                .WriteTo.File("SerilogLog.txt",
                    outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                    .CreateLogger();
            Log.Logger = log;
        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SubcutaneousTestsAutofacModule>();
        }
    }
}
