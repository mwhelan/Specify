using System.Collections.Generic;
using System.Linq;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiTemplate.Api.Contracts.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

        public ErrorResponse() { }

        public ErrorResponse(ResultBase result)
        {
            Message = result.Errors[0].Message;
            foreach (var error in result.GetFailures())
            {
                Errors.Add(new ErrorModel(error.PropertyName, error.Message, error.RowKey));
            }
        }

        public ErrorResponse(ModelStateDictionary modelState)
        {
            Message = "One or more validation failures have occurred.";
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors
                    .Select(x => new ErrorModel(key, x.ErrorMessage)))
                .ToList();
        }
    }

    public class ErrorModel
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
        public int RowKey { get; set; }


        public ErrorModel(string propertyName, string message, int rowKey = int.MinValue)
        {
            PropertyName = propertyName;
            Message = message;
            RowKey = rowKey;
        }
    }
}