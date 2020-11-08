using ApiTemplate.Api.Domain.Common.FluentResult;
using FluentResults;

namespace ApiTemplate.Api.Application.Common.Paging
{
    public class PagedQuery
    {
        public int Page { get; set; } = 1;
        public int? PageSize { get; set; } = null;
        public string OrderBy { get; set; } = null;
        public bool OrderByDesc { get; set; } = false;
        public string FilterField { get; set; } = null;
        public string FilterText { get; set; } = null;

        public bool HasSearch => FilterField != null && FilterText != null;

        public Result IsValid()
        {
            if (FilterField != null && FilterText is null)
            {
                return Resultz.Error("FilterText", ValidationMessages.IsRequiredFor(nameof(FilterText)));
            }

            return Result.Ok();
        }
    }
}
