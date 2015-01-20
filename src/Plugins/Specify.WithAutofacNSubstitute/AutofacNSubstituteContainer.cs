namespace Specify.WithAutofacNSubstitute
{
    public class AutofacNSubstituteContainer : AutofacContainer
    {
        public override ITestLifetimeScope CreateTestLifetimeScope()
        {
            return new AutofacNSubstituteTestLifetimeScope();
        }
    }
}