using Serilog;
using Specify;
using Specify.Autofac;
using Specify.Configuration;

namespace ContosoUniversity.SubcutaneousTests
{
    public class SubcutaneousConfig : SpecifyBootstrapper
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

        public override IApplicationContainer CreateApplicationContainer()
        {
            return new AutofacApplicationContainer();
        }
    }
}
