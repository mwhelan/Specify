//using Autofac;
//using Autofac.Core;
//using TestStack.ConventionTests;
//using TestStack.ConventionTests.Autofac;

//namespace Specs.Integration.ApiTemplate.Api.Configuration
//{
//    public class AutofacConventions : ScenarioFor<ILifetimeScope>
//    {
//        private AutofacRegistrations _autofacRegistrations;

//        public override string Title => "Autofac Container";

//        public void Given_all_the_services_registered_in_Autofac_container()
//        {
//            _autofacRegistrations = new AutofacRegistrations(SUT.ComponentRegistry);
//        }

//        public void When_I_attempt_to_resolve_each_service()
//        {

//        }

//        public void Then_should_be_able_to_resolve_all_registered_services()
//        {
//            IContainer container = SUT as IContainer;
//            Convention.Is(new CanResolveAllRegisteredServices(container), _autofacRegistrations);
//        }

//        public void AndThen_services_should_only_have_dependencies_with_lesser_lifetime()
//        {
//            Convention.Is(new ServicesShouldOnlyHaveDependenciesWithLesserLifetime(), _autofacRegistrations);
//        }
//    }
//}
