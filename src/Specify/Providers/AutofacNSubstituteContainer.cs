namespace Specify.Providers
{
    internal class AutofacNSubstituteContainer : AutofacContainer
    {
        public override ITestLifetimeScope CreateTestLifetimeScope()
        {
            return new AutofacNSubstituteTestLifetimeScope();
        }
    }
}