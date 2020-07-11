using System.Collections.Generic;
using System.Linq;
using ApiTemplate.Api.Application.Common.Validation;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentAssertions;
using FluentValidation;
using Specify;
using Specs.Library.Builders;
using Specs.Library.Builders.ObjectMothers;
using TestStack.BDDfy;
using Result = FluentResults.Result;

namespace Specs.Unit.ApiTemplate.Application.Common.Validation
{
    public abstract class ValidationServiceScenario : ScenarioFor<ValidationService>
    {
        protected Result Result;
        protected TestCommand Command;

        public virtual void Setup()
        {
            SUT = new ValidationService(new List<IValidator>() { new TestCommandValidator() });
        }
    }

    public class SucceedsIfNoValidator : ValidationServiceScenario
    {
        public override void Setup()
        {
            SUT = new ValidationService(new List<IValidator>());
        }

        public void When_the_command_is_validated_with_no_validators()
        {
            Result = SUT.ValidateCommand(new TestCommand{IgnoreWarnings = false});
        }

        public void Then_the_command_is_determined_to_be_valid()
        {
            Result.IsSuccess.Should().BeTrue();
        }
    }

    public class SucceedsIfValidCommandRegardlessOfIgnoreWarnings : ValidationServiceScenario
    {
        public SucceedsIfValidCommandRegardlessOfIgnoreWarnings()
        {
            Examples = new ExampleTable("Ignore Warnings") { true, false };
        }

        public void Given_a_valid_command(bool ignoreWarnings)
        {
            Command = Get.StubFor.TestCommand(ignoreWarnings);
        }

        public void When_the_command_is_validated()
        {
            Result = SUT.ValidateCommand(Command);
        }

        public void Then_the_command_is_determined_to_be_valid()
        {
            Result.IsSuccess.Should().BeTrue();
            Result.IsFailed.Should().BeFalse();
            Result.HasWarnings().Should().BeFalse();
            Result.GetErrors().Should().HaveCount(0);
        }
    }

    public class SucceedsIfWarningsAndIgnoreWarningsTrue : ValidationServiceScenario
    {
        public void Given_an_invalid_command_that_generates_Warnings()
        {
            Command = Get.StubFor.TestCommand(true)
                .With(x => x.Reference = string.Empty)
                .With(x => x.IgnoreWarnings = true);
        }

        public void When_the_command_is_validated_with_warnings_ignored()
        {
            Result = SUT.ValidateCommand(Command);
        }

        public void Then_the_command_is_determined_to_be_valid()
        {
            Result.IsSuccess.Should().BeTrue();
            Result.IsFailed.Should().BeFalse();
            Result.HasWarnings().Should().BeTrue();
            Result.GetWarnings().Should().HaveCount(1);
            Result.GetWarnings().First().PropertyName.Should().Be(nameof(Command.Reference));
        }
    }

    public class FailsIfWarningsAndIgnoreWarningsFalse : ValidationServiceScenario
    {
        public void Given_an_invalid_command_that_generates_Warnings()
        {
            Command = Get.StubFor.TestCommand().With(x => x.Reference = string.Empty);
        }

        public void When_the_command_is_validated_with_warnings_acknowledged()
        {
            Result = SUT.ValidateCommand(Command);
        }

        public void Then_the_command_is_determined_to_be_invalid()
        {
            Result.IsSuccess.Should().BeFalse();
            Result.IsFailed.Should().BeTrue();
            Result.GetWarnings().Count.Should().Be(1);
        }
    }

    public class FailsIfErrorsRegardlessOfIgnoreWarnings : ValidationServiceScenario
    {
        public FailsIfErrorsRegardlessOfIgnoreWarnings()
        {
            Examples = new ExampleTable("Ignore Warnings") { true, false };
        }

        public void Given_an_invalid_command_that_generates_Warnings(bool ignoreWarnings)
        {
            Command = Get.StubFor.TestCommand(ignoreWarnings).With(x => x.Name = string.Empty);
        }

        public void When_the_command_is_validated()
        {
            Result = SUT.ValidateCommand(Command);
        }

        public void Then_the_command_is_determined_to_be_valid()
        {
            Result.IsSuccess.Should().BeFalse();
            Result.IsFailed.Should().BeTrue();
            Result.GetErrors().Should().HaveCount(1);
        }
    }
}
