using System.Diagnostics.CodeAnalysis;
using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;

namespace ApiTemplate.Api.Domain.Common.Guards
{
    /// <summary>
    /// An entry point to a set of Guard Clauses.
    /// </summary>
    public class Ensure
    {
        private Result _result = Result.Ok();
        private readonly int _rowKey;

        private Ensure(int rowKey = int.MinValue)
        {
            _rowKey = rowKey;
        }

        public static Ensure That()
        {
            return new Ensure();
        }

        public static Ensure That(int rowKey)
        {
            return new Ensure(rowKey);
        }

        public Result ToResult()
        {
            return _result;
        }

        public Result<T> ToResult<T>()
        {
            return _result.ToResult<T>();
        }


        public Ensure NotNull([NotNull] object? input, string parameterName,
            ValidationSeverity validationSeverity = ValidationSeverity.Error)
        {
            if (null != input) return this;

            var message = $"Required input '{parameterName}' was null.";
            _result.Reasons.Add(Resultz.GetReasonForFailure(validationSeverity, parameterName, message, _rowKey));

            return this;
        }

        public Ensure Same([NotNull] object? input, [NotNull] object? existing , string parameterName,
            ValidationSeverity validationSeverity = ValidationSeverity.Error)
        {
            if (input == existing) return this;

            var message = $"Input '{parameterName}' has changed.";
            _result.Reasons.Add(Resultz.GetReasonForFailure(validationSeverity, parameterName, message, _rowKey));

            return this;
        }

        public Ensure NotNullOrEmpty([NotNull] string? input, string parameterName,
            ValidationSeverity validationSeverity = ValidationSeverity.Error)
        {
            if (!string.IsNullOrEmpty(input)) return this;

            var message = $"Required input '{parameterName}' was empty.";
            _result.Reasons.Add(Resultz.GetReasonForFailure(validationSeverity, parameterName, message, _rowKey));

            return this;
        }

        public Ensure NotNullOrZero([NotNull] int? input, string parameterName,
            ValidationSeverity validationSeverity = ValidationSeverity.Error)
        {
            if (input == null || input == 0)
            {
                var message = $"Required input '{parameterName}' was null or 0.";
                _result.Reasons.Add(Resultz.GetReasonForFailure(validationSeverity, parameterName, message, _rowKey));
            }

            return this;
        }

        public Ensure GreaterThanZero([NotNull] decimal? input, string parameterName,
            ValidationSeverity validationSeverity = ValidationSeverity.Error)
        {
            if (input == null || input <= 0)
            {
                var message = $"'{parameterName}' must be greater than zero.";
                _result.Reasons.Add(Resultz.GetReasonForFailure(validationSeverity, parameterName, message, _rowKey));
            }

            return this;
        }

        public Ensure MutuallyExclusiveRequired([NotNull] object? input1, [NotNull] object? input2, 
            string parameterName1, string parameterName2,
            ValidationSeverity validationSeverity = ValidationSeverity.Error)
        {
            if (input1 == null && input2 == null)
            {
                var message = $"Either input '{parameterName1}' or '{parameterName2}' is required.";
                _result.Reasons.Add(Resultz.GetReasonForFailure(validationSeverity, parameterName1, message, _rowKey));
            }

            if (input1 != null && input2 != null)
            {
                var message = $"Cannot enter both inputs '{parameterName1}' and '{parameterName2}'.";
                _result.Reasons.Add(Resultz.GetReasonForFailure(validationSeverity, parameterName1, message, _rowKey));
            }

            return this;
        }

        //public Ensure MaximumLength([NotNull] string? input, string parameterName, int maximumLength)
        //{
        //    if (input?.Length > maximumLength)
        //    {
        //        _validationReason.Add(parameterName, $"The length of '{parameterName}' must be {maximumLength} characters or fewer. You entered {parameterName.Length} characters.");
        //    }

        //    return this;
        //}

    }
}
