# Supported Test Frameworks
Like BDDfy, Specify can run with any testing framework. Well, NUnit, xUnit, MsTest and Fixie anyway, which are the ones I've tried it with. 

If you are using [Fixie](http://fixie.github.io/), then you can use Specify as is, and you just have to provide a convention to tell it how to disover and run your specifications.

If you are using NUnit, xUnit, or MsTest, then you need to provide the attributes that they need to discover and run your specifications. The good news is that you only have to provide this information once, on your base class - all of the specifications that you write will inherit from this class and not need any attributes. 

At this stage, I've decided not to release separate NuGet packages for each test framework. I don't think it's worth it for a few lines of code. To use NUnit, for example, you just need to copy this code into your project, which inherits from the two Specify base classes and adds the NUnit test attributes.

In the following pages I will provide the "glue code" that is required to wire up the different test frameworks. This code is also available in the Specify.Examples project, which is available as part of the source code on [github](https://github.com/mwhelan/Specify/tree/master/src/Specify.Examples).

## xUnit
If you are using xUnit, then you need to provide the attributes that it needs to discover and run your specifications. The good news is that you only have to provide this information once, on your base class - all of the specifications that you write will inherit from this class and not need any attributes.

	public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
	    where TSut : class
	    where TStory : Story, new()
	{
	    [Fact]
	    public override void Specify()
	    {
	        base.Specify();
	    }
	}
	
	public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
	    where TSut : class
	{
	    [Fact]
	    public override void Specify()
	    {
	        base.Specify();
	    }
	}

## NUnit
If you are using NUnit, then you need to provide the attributes that it needs to discover and run your specifications. The good news is that you only have to provide this information once, on your base class - all of the specifications that you write will inherit from this class and not need any attributes.

	[TestFixture]
	public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
	    where TSut : class
	    where TStory : Story, new()
	{
	    [Test]
	    public override void Specify()
	    {
	        base.Specify();
	    }
	}
	
	[TestFixture]
	public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> 
	    where TSut : class
	{
	    [Test]
	    public override void Specify()
	    {
	        base.Specify();
	    }
	}

## MsTest
If you are using MsTest, then you need to provide the attributes that it needs to discover and run your specifications. The good news is that you only have to provide this information once, on your base class - all of the specifications that you write will inherit from this class and not need any attributes.

	[TestClass]
	public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
	    where TSut : class
	    where TStory : Story, new()
	{
	    [TestMethod]
	    public override void Specify()
	    {
	        base.Specify();
	    }
	}

	[TestClass]
	public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
	    where TSut : class
	{
	    [TestMethod]
	    public override void Specify()
	    {
	        base.Specify();
	    }
	}

## Fixie
To use Specify with Fixie, you simply have to provide a Fixie convention class. The convention that we need to apply is that the class implements `IScenario` and the test method is named `Specify`.

	public class FixieSpecifyConvention : Convention
	{
	    public FixieSpecifyConvention()
	    {
	        Classes
	            .Where(type => type.IsScenario());
	
	        Methods
	            .Where(method => method.Name == "Specify");
	    }
	}