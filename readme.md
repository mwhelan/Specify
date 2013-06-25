# What is it?
Specify is a small opinionated testing library for writing different kinds of tests in .Net. It is built on top of several other libraries:

* BDDfy
* Seleno
* NSubstitute
* Castle Windsor (soon to be replaced by Autofac)
* Selenenium
* Your choice of testing framework (NUnit, xunit, MsTest....)

I have been using mspec/Machine Fakes/Moq for unit and integration testing and NUnit/BDDfy/Seleno/Castle Windsor for executable specifications and Selenium UI testing. I would like to use NUnit and BDDfy across the board, and stop using mspec. I really like mspec a lot but I'd rather have a consistent approach, with one set of extension methods to maintain, etc.


# Unit/Integration tests
Specify attempts to replicate the Machine Fakes library for mspec by providing mocking and auto-mocking to (x)unit frameworks using NSubstitute and Castle Windsor.


## WithFakes (from the [Machine Fakes](https://github.com/BjRo/Machine.Fakes) website)
Let's take a look at the simpler one first. By deriving from this class you can use the An<<TFake>>() and Some<<TFake>>() (*) methods for creating fakes as well as the extension methods based API for setting up the behavior (**). The WithFakes class only provides the basic fake framework abstraction.


    public class Given_the_current_day_is_monday_when_identifying_my_mood : WithFakes
    {
        static MoodIdentifier _subject;
        static string _mood;

        Establish context = () =>
        {
            var monday = new DateTime(2011, 2, 14);
            var systemClock = An<ISystemClock>(); (*)

            systemClock
                .WhenToldTo(x => x.CurrentTime) (**)
                .Return(monday);

            _subject = new MoodIdentifier(systemClock);
        };

        Because of = () => _mood = _subject.IdentifyMood();

        It should_be_pretty_bad = () => _mood.ShouldEqual("Pretty bad");
    }

WhenToldTo is used for setting up a behavior but Machine.Fakes also supports behavior verification on fakes with the WasToldTo and WasNotToldTo extension methods.

    public class When_a_method_is_called_on_a_fake : WithFakes
    {
        static IServiceContainer_subject;

        Establish context = () =>
        {
            _subject = An<IServiceContainer>();
        };

        Because of = () => _subject.GetService(null);

        It should_be_able_to_verify_that = () => _subject.WasToldTo(s => s.GetService(null));
    }

## WithSubject<<TSubject>>

Back to our example with the MoodIdentifier. Do we really need to create the subject of the specification by hand? Can we make it even more simpler? Yes, by introducing the concept of an AutoMockingContainer to the specification. That's exactly what WithSubject<TSubject> does. Here's a modified example.

    public class Given_the_current_day_is_monday_when_identifying_my_mood : WithSubject<MoodIdentifier> (*)
    {
        static string _mood;

        Establish context = () =>
        {
            var monday = new DateTime(2011, 2, 14);

            The<ISystemClock>() (**)
                .WhenToldTo(x => x.CurrentTime)
                .Return(monday);
        };

        Because of = () => _mood = Subject.IdentifyMood(); (***)

        It should_be_pretty_bad = () => _mood.ShouldEqual("Pretty bad");
    }

The generic type parameter (*) tells Machine.Fakes what type to create for the specification. Each interface or abstract base class dependency in the constructor of the type will be filled automatically by the configured fake framework. Dependency Injection with fakes so to speak.

You can access the created instance through the lazy "Subject" property (***). The actual subject is created on the first read access to this property. If you want to modify the subject when the context is established, go ahead, you can do so. You can even replace the subject by hand in case the automocking approach falls short.

Having the subject created for us is a good thing but how do we access the injected fake without having a reference to it? That's exactly the purpose of the The<<TFake>>() method (**) which gives access to the injected dependency.


# Subcutaneous tests (in ASP.Net MVC) 
Uses the base ScenarioFor class with various types of MVC controllers as the SUT.

# Selenium Functional Tests
Uses the base ScenarioFor class with a dedicated BrowserScenario that utitlises Seleno and Selenium.
