using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentAssertions;
using FluentResults;
using NUnit.Framework.Internal;
using Specs.Library.Builders;

namespace Specs.Unit.ApiTemplate.Domain.Common.FluentResult
{
    public class WhenGettingFailures : ScenarioFor<Result>
    {
        public void When_evaluating_a_Result_with_every_Application_Error_And_Warning()
        {
            SUT = Get.StubFor.ResultWithErrorsAndWarnings();
        }

        public void Then_GetFailures_should_only_return_Application_Errors_and_Warning()
        {
            SUT.GetFailures().Should().HaveCount(3);
        }

        public void AndThen_GetErrors_should_only_return_Application_Errors()
        {
            SUT.GetErrors().Should().HaveCount(2);
        }

        public void AndThen_GetWarnings_should_only_return_Application_Warnings()
        {
            SUT.GetWarnings().Should().HaveCount(1);
        }
    }

    public class WhenCheckingIfResultHasFailures : ScenarioFor<Result>
    {
        public void Then_Result_with_no_Errors_or_Warnings_should_not_have_failures()
        {
            Result.Ok().HasFailures().Should().BeFalse();
        }

        public void AndThen_Result_with_Errors_and_Warnings_should_have_failures()
        {
            Resultz.Warning("")
                .WithReason(new RecordsNotFoundAppError("Id", 22))
                .HasFailures().Should().BeTrue();
        }

        public void AndThen_Result_with_only_Warnings_should_have_failures()
        {
            Resultz.Warning("").HasFailures().Should().BeTrue();
        }

        public void AndThen_Result_with_only_Errors_should_have_failures()
        {
            Resultz.Error("").HasFailures().Should().BeTrue();
        }
    }

    public class WhenCheckingIfResultHasErrors : ScenarioFor<Result>
    {
        public void Then_Result_with_Errors_should_have_Errors()
        {
            Resultz.Error("")
                .HasErrors().Should().BeTrue();
        }

        public void AndThen_Result_without_Errors_should_not_have_Errors()
        {
            Resultz.Warning("")
                .HasErrors().Should().BeFalse();
        }
    }

    public class WhenCheckingIfResultHasWarnings : ScenarioFor<Result>
    {
        public void Then_Result_with_Warnings_should_have_Warnings()
        {
            Resultz.Warning("")
                .HasWarnings().Should().BeTrue();
        }

        public void AndThen_Result_without_Warnings_should_not_have_Warnings()
        {
            Result.Ok().HasWarnings().Should().BeFalse();
        }
    }

    public class WhenAddingResult : ScenarioFor<Result>
    {
        private Result _original;
        private Result _new;
        private Result _result;

        public void Given_two_Results()
        {
            _original = Get.StubFor.ResultWithErrorsAndWarnings();
            _new = Get.StubFor.ResultWithErrorsAndWarnings();
        }

        public void When_adding_one_Result_to_the_other()
        {
            SUT = Get.StubFor.ResultWithErrorsAndWarnings();
            _result = SUT.AddResult(_new);
        }

        public void Then_the_new_Result_Reasons_are_added_to_the_original_Result()
        {
            _result.Reasons.Should().HaveCount(_original.Reasons.Count + _new.Reasons.Count);
        }

        public void AndThen_the_new_Result_Errors_are_added_to_the_original_Result()
        {
            _result.GetErrors().Should().HaveCount(_original.GetErrors().Count + _new.GetErrors().Count);
        }

        public void Then_the_new_Result_Warnings_are_added_to_the_original_Result()
        {
            _result.GetWarnings().Should().HaveCount(_original.GetWarnings().Count + _new.GetWarnings().Count);
        }
    }

    public class AddingErrorsAndWarnings : ScenarioFor<Result>
    {
        public void Then_AddError_with_property_should_include_property()
        {
            (Result.Ok()
                .AddError("propertyName", "message", 99)
                .Errors[0] as AppError)
                .Should().BeEquivalentTo(new AppError("propertyName", "message", 99));
        }

        public void Then_AddError_without_property_should_not_include_property()
        {
            (Result.Ok()
                    .AddError("message", 99)
                    .Errors[0] as AppError)
                .Should().BeEquivalentTo(new AppError("message", 99));
        }

        public void Then_AddWarning_with_property_should_include_property()
        {
            (Result.Ok()
                    .AddWarning("propertyName", "message", 99)
                    .GetWarnings()[0] as AppWarning)
                .Should().BeEquivalentTo(new AppWarning("propertyName", "message", 99));
        }

        public void Then_AddWarning_without_property_should_not_include_property()
        {
            (Result.Ok()
                    .AddWarning("message", 99)
                    .GetWarnings()[0] as AppWarning)
                .Should().BeEquivalentTo(new AppWarning("message", 99));
        }
    }
}