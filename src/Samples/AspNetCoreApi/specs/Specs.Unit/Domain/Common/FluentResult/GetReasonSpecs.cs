using System;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentAssertions;
using FluentResults;

namespace Specs.Unit.ApiTemplate.Domain.Common.FluentResult
{
    public class GetReason : ScenarioFor<Result>
    {
        public void Given_a_Result_with_a_Reason()
        {
            SUT  = Results.Ok().WithReason(new RecordsNotFoundAppError("Id", 22));
        }

        public void When_GetReason_is_called_for_that_type_of_Reason()
        {

        }

        public void Then_should_return_requested_Reason()
        {
            var reason = SUT.GetReason<RecordsNotFoundAppError>();
            reason.Should().NotBeNull();
            reason.Should().BeOfType<RecordsNotFoundAppError>();
        }

        public void AndThen_should_throw_if_requested_Reason_missing()
        {
            Action action = () => SUT.GetReason<RecordsCreatedSuccess>();
            action.Should().Throw<InvalidOperationException>();
        }

    }
}
