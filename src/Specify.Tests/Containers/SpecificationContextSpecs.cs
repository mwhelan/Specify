//using NSubstitute;
//using NUnit.Framework;
//using Specify.Containers;

//namespace Specify.Tests.Containers
//{
//    public class SpecificationContextSpecs
//    {
//        [Test]
//        public void should_dispose_dependency_scope_after_specification_completes()
//        {
//            var dependencyScope = Substitute.For<IDependencyResolver>();
//            using (var context = new SpecificationContext<object>(dependencyScope))
//            {
                
//            }
//            dependencyScope.Received().Dispose();
//        }
//    }
//}