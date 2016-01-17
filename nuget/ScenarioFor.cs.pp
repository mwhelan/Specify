// -------------------------------------------------------------------------------------------------------------------
// These are the base classes for Specify scenarios. 
// The ScenarioFor<TSut> class is for unit tests and the ScenarioFor<TSut, TStory> class is for acceptance tests.
//
// To use Specify with NUnit, xUnit, or MsTest:
// 1) Uncomment the lines for your test framework.
// 2) Have your Specify scenarios inherit from one of these two classes.
//
// To use Specify with Fixie:
// 1) Uncomment the using statement for Fixie and the FixieSpecifyConvention class.
// 2) Have your Specify scenarios inherit from the ScenarioFor classes in Specify.
// -------------------------------------------------------------------------------------------------------------------

//using NUnit.Framework;                                // NUnit
//using Xunit;                                          // xUnit
//using Microsoft.VisualStudio.TestTools.UnitTesting;   // MsTest
//using Fixie;
using Specify.Stories;

namespace $rootnamespace$
{
    /// <summary>
    /// The base class for scenarios without a story (normally unit test scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    //[TestFixture] // NUnit
    //[TestClass]   // MsTest
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
        where TSut : class
    {
        //[Test]        // NUnit
        //[Fact]        // xUnit
        //[TestMethod]  // MsTest
        public override void Specify()
        {
            base.Specify();
        }
    }

    /// <summary>
    /// The base class for scenarios with a story (BDD-style acceptance tests).
    /// </summary>
    /// <typeparam name="TSut">The type of the SUT.</typeparam>
    /// <typeparam name="TStory">The type of the t story.</typeparam>
    //[TestFixture] // NUnit    
    //[TestClass]   // MsTest
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
        //[Test]        // NUnit
        //[Fact]        // xUnit
        //[TestMethod]  // MsTest
        public override void Specify()
        {
            base.Specify();
        }
    }
    
//    public class FixieSpecifyConvention : Convention
//    {
//        public FixieSpecifyConvention()
//        {
//            Classes
//                .Where(type => type.IsScenario());

//            Methods
//                .Where(method => method.Name == "Specify");
//        }
//    }
    
}