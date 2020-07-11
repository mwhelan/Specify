using ApiTemplate.Api.Application.Common.Interfaces;

namespace Specs.Library.Identity
{
    public class TestCurrentUserService : ICurrentUserService
    {
        public int UserId => 1;
    }
}