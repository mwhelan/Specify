## Summary
1. Install Specify from NuGet
2. Wire up your test framework - NUnit, xUnit, MsTest, or Fixie
3. Install your preferred mocking framework or IoC container
4. Optionally, customise Specify with a bootstrapper class

## 1. Install Specify
Specify is available as a [NuGet package](https://www.nuget.org/packages/Specify/). You can add it via the `Manage NuGet Packages` dialog or by the `Package Manager Console`.

NuGet will install Specify and the latest version of TestStack.BDDfy. 
## 2. Wire Up Your Test Framework
Like BDDfy, Specify can run with any testing framework. Well, NUnit, xUnit, MsTest and Fixie anyway, which are the ones I've tried it with. 

Create a **ScenarioFor.cs** file with the base classes that all your scenarios will inherit from. These are just wrappers around the real ScenarioFors in Specify with the sole function of wiring up your test framework. 

If you are using NUnit, xUnit, or MsTest, then you need to provide the attributes that they need to discover and run your specifications. The good news is that you only have to provide this information once, on your base class - all of the specifications that you write will inherit from this class and not need any attributes. 

Up until now, I had decided not to release separate NuGet packages for each test framework. With previous versions of .Net I would inject this file into the test project during installation of the NuGet package. This feature is currently unavailable for .Net Core. 

For now,  you need to copy the classes below into your test project, depending which test framework you are using. I will probably release these as NuGet packages in the future. 

### NUnit
```csharp
using NUnit.Framework;                                
using Specify.Stories;

namespace ClassLibrary1
{
    /// <summary>
    /// The base class for scenarios without a story (normally unit test scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
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

    /// <summary>
    /// The base class for scenarios with a story (BDD-style acceptance tests).
    /// </summary>
    /// <typeparam name="TSut">The type of the SUT.</typeparam>
    /// <typeparam name="TStory">The type of the t story.</typeparam>
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
}
```

### xUnit
```csharp
using Xunit;                                          ;
using Specify.Stories;

namespace ClassLibrary1
{
    /// <summary>
    /// The base class for scenarios without a story (normally unit test scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    //[TestFixture] 
    //[TestClass]   
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
        where TSut : class
    {
        [Fact]        
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
}
```

### MsTest
```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;   
using Specify.Stories;

namespace ClassLibrary1
{
    /// <summary>
    /// The base class for scenarios without a story (normally unit test scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
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

    /// <summary>
    /// The base class for scenarios with a story (BDD-style acceptance tests).
    /// </summary>
    /// <typeparam name="TSut">The type of the SUT.</typeparam>
    /// <typeparam name="TStory">The type of the t story.</typeparam>
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
}
```

### Fixie
If you are using [Fixie](http://fixie.github.io/), then you can use Specify as is, and you just have to provide a convention to tell it how to discover and run your specifications.
```csharp
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
```

## 3. Install your preferred mocking framework or IoC container
### For Unit Test projects
Install one of these mocking frameworks:

- NSubstitute
- FakeItEasy
- Moq

	Install-Package NSubstitute
	Install-Package FakeItEasy
	Install-Package Moq

Alternatively, you could provide a simple adapter and plug in another mocking framework.

### For Integration Test projects
Install the IoC container that your application is using. If you use Autofac then you can use the Specify.Autofac package. For other IoC containers you will have to write an adapter class.

## 4. Optionally, customise Specify with a bootstrapper class
### For Unit Test projects
If you want to configure Specify for unit test projects then you can create a bootstrapper class to configure Specify with the built-in [TinyIoc](https://github.com/grumpydev/TinyIoC) container. Make any changes to the default configuration settings in this file.

```csharp
using Specify.Configuration;
using TinyIoC;

namespace ClassLibrary1
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyBootstrapper : DefaultBootstrapper
    {
        /// <summary>
        /// Register any additional items into the TinyIoc container or leave it as it is. 
        /// </summary>
        /// <param name="container">The <see cref="TinyIoCContainer"/> container.</param>
        public override void ConfigureContainer(TinyIoCContainer container)
        {

        }
    }
}
```

### For Integration Test projects
If you want to configure Specify for integration test projects then you can create a bootstrapper class to configure Specify with the IoC container. Make any changes to the default configuration settings in this file.

For example, with the [Specify.Autofac](https://www.nuget.org/packages/Specify.Autofac/) package you would use this bootstrapper file:

```csharp
using Autofac;
using Specify.Autofac;

namespace ClassLibrary1
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyAutofacBootstrapper : DefaultAutofacBootstrapper
    {
        /// <summary>
        /// Register any additional items into the Autofac container using the ContainerBuilder or leave it as it is. 
        /// </summary>
        /// <param name="builder">The Autofac <see cref="ContainerBuilder"/>.</param>
        public override void ConfigureContainer(ContainerBuilder builder)
        {

        }
    }
}
```