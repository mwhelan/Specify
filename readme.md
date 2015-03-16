# Specify

## What is it?
Specify is a .Net context-specification testing library that builds on top of BDDfy from [TestStack](http://teststack.net/). While BDDfy is primarily intended for BDD testing, it is beautifully designed to be very easy to customize and extend.

When I first started using BDDfy for acceptance testing, I would use a different framework for unit testing, but I didn't like the context switching between different frameworks, syntaxes and testing styles. The goal of Specify is to provide a consistent experience for all types of tests. Why not have the fantastic BDDfy reports for all of your different test types?

[![Build status](https://ci.appveyor.com/api/projects/status/vj6ec2yubg8ii9sn?svg=true)](https://ci.appveyor.com/project/mwhelan/specify)

## Overview of Features
* Tests use a context-specification style, with a class per test.
* Supports automocking containers for unit tests and IoC containers for larger tests.
* Tests can be resolved from your IoC container.
* User stories can be dynamically applied to your tests [Not implemented yet].
* Reports are produced for all of your test types

## Context-Specification Style
With context-specification you have a class per test, with each step having its own method and state being shared between methods in fields. This means that the setup and execution only happen once, and then each `Then` method is a test that asserts against the result of the execution.

Specify provides two generic base classes that your test class can inherit from. 
* SpecificationFor<T> is for unit tests and would normally be used with an automocking container.
* ScenarioFor<T> is for acceptance tests and would normally be used with an Inversion of Control container.

These classes follow the `Given When Then` syntax (though there is nothing to stop you from customizing BDDfy to use a different syntax if you want). 
 
    public class DetailsForExistingStudent : SpecificationFor<StudentController>
    {
        ViewResult _result;
        private Student _student = new Student { ID = 1 };

        public void Given_an_existing_student()
        {
            Get<ISchoolRepository>()
                .FindStudentById(_student.ID)
                .Returns(_student);
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _result = SUT.Details(_student.ID) as ViewResult;
        }

        public void Then_the_details_view_is_displayed()
        {
            _result.ViewName.Should().Be(string.Empty);
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            _result.Model.Should().Be(_student);
        }
    }


### System Under Test
The type parameter on the base classes represents the [System Under Test](http://xunitpatterns.com/SUT.html), which is the class that is being tested. Each class has a `SUT` property, which is instantiated for you by the automocking or IoC container. You can override this too, if you want.

### Test Lifecycle
Specify uses BDDfy's Reflective API to scan its classes for methods. By default, BDDfy recognises the standard BDD methods, as well as Setup and TearDown. You can read more about them [here](http://www.mehdi-khalili.com/bddify-in-action/method-name-conventions) and you can always customize them if you have your own preferences.


Specify provides two virtual methods that you can override and that will run before the BDDfy methods:

* **ConfigureContainer**: The System Under Test is created by the Container so this method gives you a chance to inject items into the Container that will be used in the construction of the SUT.
* **CreateSystemUnderTest**: There are times when you want to create the SUT yourself, rather than letting the Container create it.  

### Containers
The SpecificationFor class has an automocking container and the ScenarioFor class has an IoC container. A fresh new container is provided for each specification, so you can change the configuration for each test without impacting other tests. Most IoC containers provide this child container functionality (though they might call it by different names).

The Specification classes allow you to interact with the Container through the following properties:

* **Get<T>(string key = null)**: Gets a value of the specified type from the container, optionally registered under a key.
* **Set<T>(T valueToSet, string key = null)**: Sets a value in the container to a concrete instance.
* **Container**: The full Container provides more functionality.

You can write an adapter for your own container by implementing this interface:

    public interface ITestLifetimeScope : IDisposable
    {
        T SystemUnderTest<T>() where T : class;
        void RegisterType<T>() where T : class;
        T RegisterInstance<T>(T valueToSet, string key = null) where T : class;
        T Resolve<T>(string key = null) where T : class;
        bool IsRegistered<T>() where T : class;
        bool IsRegistered(Type type);
    }

The containers are largely based on [Chill's containers](https://github.com/Erwinvandervalk/Chill), by [Erwin van der Valk](http://www.erwinvandervalk.net/). Chill is a great framework, which I recommend you check out.

## Unit Tests
You can read more about the unit testing approach [here](http://www.michael-whelan.net/using-bddfy-for-unit-tests/):
