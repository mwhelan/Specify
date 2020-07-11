using System.Linq;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentAssertions;
using FluentResults;

namespace Specs.Unit.ApiTemplate.Application.Common.Validation
{
    public class MergeTwoSuccessResults : ScenarioFor<Result>
    {
        private Result _okResult1;
        private Result _okResult2;

        public void Given_two_success_results()
        {
            _okResult1 = Results.Ok().WithReason(new AppWarning("Property1", "Message1"));
            _okResult2 = Results.Ok().WithReason(new AppWarning("Property2", "Message2"));
        }

        public void When_they_are_merged()
        {
            SUT = Results.Merge(_okResult1, _okResult2);
        }

        public void Then_status_should_be_Ok()
        {
            SUT.IsSuccess.Should().BeTrue();
        }

        public void AndThen_the_reasons_should_be_combined()
        {
            SUT.GetWarnings().Count.Should().Be(2);
            SUT.GetWarnings().First().PropertyName.Should().Be("Property1");
            SUT.GetWarnings().Last().PropertyName.Should().Be("Property2");
        }
    }

    public class MergeSuccessAndFailResults : ScenarioFor<Result>
    {
        private Result _okResult;
        private Result _failResult;

        public void Given_one_success_and_one_fail_result()
        {
            _okResult = Results.Ok().WithReason(new AppWarning("Property1", "Message1"));
            _failResult = Results.Ok().WithReason(new AppError("Property2", "Message2"));
        }

        public void When_they_are_merged()
        {
            SUT = Results.Merge(_okResult, _failResult);
        }

        public void Then_status_should_be_Fail()
        {
            SUT.IsFailed.Should().BeTrue();
        }

        public void AndThen_the_reasons_should_be_combined()
        {
            SUT.GetWarnings().Should().HaveCount(1);
            SUT.GetErrors().Should().HaveCount(1);
            SUT.GetWarnings().First().PropertyName.Should().Be("Property1");
            SUT.GetErrors().Last().PropertyName.Should().Be("Property2");
        }
    }
}
