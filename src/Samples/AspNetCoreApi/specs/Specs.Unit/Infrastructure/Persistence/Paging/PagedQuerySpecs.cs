using ApiTemplate.Api.Application.Common.Paging;
using FluentAssertions;
using Specify;

namespace Specs.Unit.ApiTemplate.Infrastructure.Persistence.Paging
{
    public class PagedQuerySpecs : ScenarioFor<PagedQuery>
    {
        private PagedQuery _defaultQuery;
        private PagedQuery _filterFieldOnly;
        private PagedQuery _filterFieldAndText;

        public void When_creating_a_PagedQuery()
        {
            _defaultQuery = new PagedQuery();
            _filterFieldOnly = new PagedQuery()
                .With(x => x.FilterField = "F");
            _filterFieldAndText = new PagedQuery()
                .With(x => x.FilterField = "F")
                .With(x => x.FilterText = "T");
        }

        public void Then_should_only_have_search_if_both_FilterField_and_FilterText_provided()
        {
            _defaultQuery.HasSearch.Should().BeFalse();
            _filterFieldOnly.HasSearch.Should().BeFalse();
            _filterFieldAndText.HasSearch.Should().BeTrue();
        }

        public void AndThen_should_only_be_invalid_if_FilterField_set_and_FilterText_not()
        {
            _defaultQuery.IsValid().IsSuccess.Should().BeTrue();
            _filterFieldOnly.IsValid().IsSuccess.Should().BeFalse();
            _filterFieldAndText.IsValid().IsSuccess.Should().BeTrue();
        }
    }
}
