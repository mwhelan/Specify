using System.Linq;
using ApiTemplate.Api.Domain.Common.FluentResult;
using ApiTemplate.Api.Domain.Common.Guards;
using FluentAssertions;
using FluentResults;
using Specs.Library.Builders;
using Specs.Library.Builders.ObjectMothers;

namespace Specs.Unit.ApiTemplate.Domain.Common
{
    public abstract class EnsureScenario : ScenarioFor<Ensure>
    {
        protected ResultBase Result;
        protected TestCommand Command;
        protected const int RowKey = 99;

        protected void ShouldFail(string propertyName, string message,
            ValidationSeverity validationSeverity = ValidationSeverity.Error)
        {

            IFailure reason;
            if (validationSeverity == ValidationSeverity.Error)
            {
                reason = Result.GetReason<AppError>();
                Result.IsFailed.Should().BeTrue();
                Result.Reasons.OfType<AppError>().Should().HaveCount(1);
            }
            else
            {
                reason = Result.GetReason<AppWarning>();
                Result.IsFailed.Should().BeFalse();
                Result.Reasons.OfType<AppWarning>().Should().HaveCount(1);
            }

            reason.PropertyName.Should().Be(propertyName);
            reason.Message.Should().Be(message);
            reason.RowKey.Should().Be(RowKey);
        }

        protected void ShouldSucceed()
        {
            Result.IsSuccess.Should().BeTrue();
            Result.Reasons.Should().HaveCount(0);
        }

    }

    public class EnsureNotNullWithNullValue : EnsureScenario
    {
        public void When_NotNull_receives_null_value()
        {
            Command = new TestCommand();
            Result = Ensure.That(RowKey)
                .NotNull(Command.Name, "Name", ValidationSeverity.Warning)
                .ToResult();
        }

        public void Then_should_fail()
        {
            ShouldFail("Name", "Required input 'Name' was null.", ValidationSeverity.Warning);
        }   
    }
    
    public class EnsureNotNullWithValue : EnsureScenario
    {
        public void When_NotNull_receives_value()
        {
            Command = new TestCommand {Name = Get.Any.Company.Name()};
            Result = Ensure.That(RowKey)
                .NotNull(Command.Name, "Name")
                .ToResult();
        }

        public void Then_should_succeed()
        {
            ShouldSucceed();
        }
    }

    // TODO: Use Examples and add both null and empty values
    public class EnsureNotNullOrEmptyWithNullOrEmptyValues : EnsureScenario
    {
        public void When_NotNull_receives_null_or_empty_value()
        {
            Command = new TestCommand {Name = string.Empty};
            Result = Ensure.That(RowKey)
                .NotNullOrEmpty(Command.Name, "Name", ValidationSeverity.Warning)
                .ToResult();
        }

        public void Then_should_fail()
        {
            ShouldFail("Name", "Required input 'Name' was empty.", ValidationSeverity.Warning);
        }
    }

    public class EnsureNotNullOrEmptyWithValue : EnsureScenario
    {
        public void When_NotNullOrEmpty_receives_value()
        {
            Command = new TestCommand { Name = Get.Any.Company.Name() };
            Result = Ensure.That(RowKey)
                .NotNullOrEmpty(Command.Name, "Name")
                .ToResult();
        }

        public void Then_should_succeed()
        {
            ShouldSucceed();
        }
    }

    // TODO: Use Examples and add both null and zero values
    public class EnsureNotNullOrZeroWithNullOrZeroValues : EnsureScenario
    {
        public void When_NotNull_receives_null_or_zero_value()
        {
            Command = new TestCommand { Id = 0 };
            Result = Ensure.That(RowKey)
                .NotNullOrZero(Command.Id, "Id", ValidationSeverity.Warning)
                .ToResult();
        }

        public void Then_should_fail()
        {
            ShouldFail("Id", "Required input 'Id' was null or 0.", ValidationSeverity.Warning);
        }
    }

    public class EnsureNotNullOrZeroWithValue : EnsureScenario
    {
        public void When_NotNullOrEmpty_receives_value()
        {
            Command = new TestCommand { Id = Get.Any.Identifier.GetHashCode() };
            Result = Ensure.That(RowKey)
                .NotNullOrZero(Command.Id, "Id")
                .ToResult();
        }

        public void Then_should_succeed()
        {
            ShouldSucceed();
        }
    }
}
