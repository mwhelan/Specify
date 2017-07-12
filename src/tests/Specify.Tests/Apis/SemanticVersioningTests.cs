//using NUnit.Framework;
//using Shouldly;
//using Specify.Autofac;

//namespace Specify.Tests.Apis
//{
//    public class SemanticVersioningTests
//    {
//#if NET46
//        [Test]
//        public void specify_has_no_public_api_changes()
//        {
//            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(IScenario).Assembly);
//            publicApi.ShouldMatchApproved();
//        }

//        [Test]
//        public void specify_autofac_has_no_public_api_changes()
//        {
//            var publicApi = PublicApiGenerator.ApiGenerator.GeneratePublicApi(typeof(AutofacContainer).Assembly);
//            publicApi.ShouldMatchApproved();
//        }
//#endif
//    }
//}
