namespace Specify.WithAutofacNSubstitute
{
    using Specify.Providers;

    public class AutofacNSubstituteContainer : AutofacContainer
    {
        public override ITestLifetimeScope CreateTestLifetimeScope()
        {
            return new AutofacNSubstituteTestLifetimeScope();
        }
    }
}