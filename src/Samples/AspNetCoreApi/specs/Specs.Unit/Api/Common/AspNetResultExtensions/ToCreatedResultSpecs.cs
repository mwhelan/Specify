using ApiTemplate.Api.Common;
using ApiTemplate.Api.Contracts.Responses;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Specs.Unit.ApiTemplate.Api.Common.AspNetResultExtensions
{
    public class FailedCreatedResult : ScenarioFor<Result>
    {
        private BadRequestObjectResult _actionResult;

        public void Given_a_FailResult_with_a_ValidationReason()
        {
            SUT = Resultz.Error("PropertyName", "ErrorMessage");
        }

        public void When_ToCreatedResult_is_called()
        {
            _actionResult = (BadRequestObjectResult)SUT.ToCreatedResult("controllerName");
        }

        public void Then_should_return_Bad_Request_400_with_validation_errors()
        {
            _actionResult.Should().NotBeNull();
            _actionResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            (_actionResult.Value as ErrorResponse).Errors[0].PropertyName.Should().Be("PropertyName");
            (_actionResult.Value as ErrorResponse).Errors[0].Message.Should().Be("ErrorMessage");
        }
    }

    public class SuccessfulCreatedResult : ScenarioFor<Result>
    {
        private CreatedAtActionResult _actionResult;

        public void Given_an_OkResult_with_a_RecordCreatedReason()
        {
            SUT = Results.Ok().WithReason(new RecordsCreatedSuccess(22));
        }

        public void When_ToCreatedResult_is_called()
        {
            _actionResult =(CreatedAtActionResult) SUT.ToCreatedResult("controllerName");
        }

        public void Then_should_return_CreatedAtActionResult_with_uri_for_new_record()
        {
            _actionResult.Should().NotBeNull();
            _actionResult.RouteValues["id"].Should().Be(22);

            _actionResult.ActionName.Should().Be("Get");
            _actionResult.ControllerName.Should().Be("controllerName");
            _actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
    }
}