using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentAssertions;
using FluentResults;

namespace Specs.Unit.ApiTemplate.Domain.Common.FluentResult
{
    public class GetReasonOrDefault : ScenarioFor<Result>
    {
        public void Given_a_Result_with_a_RecordCreatedReason()
        {
            SUT = Result.Ok().WithReason(new RecordsCreatedSuccess(22));
        }

        public void When_GetReason_is_called()
        {

        }

        public void Then_should_return_reason_if_asked_for_RecordCreatedReason()
        {
            var reason = SUT.GetReasonOrDefault<RecordsCreatedSuccess>();
            reason.Should().NotBeNull();
            reason.Should().BeOfType<RecordsCreatedSuccess>();
        }

        public void AndThen_should_return_null_if_asked_for_different_reason()
        {
            var reason = SUT.GetReasonOrDefault<RecordsNotFoundAppError>();
            reason.Should().BeNull();
        }
    }
}
