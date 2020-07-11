using System.Security.Claims;
using ApiTemplate.Api.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ApiTemplate.Api.Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        private const string UserIdClaim = "UserID";

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(UserIdClaim);
            if(!string.IsNullOrEmpty(userId))
            {
                UserId = int.Parse(userId);
            }
        }

        public int UserId { get; }
    }
}