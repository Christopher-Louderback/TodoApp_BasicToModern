using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Todo.Services.Helpers
{
    public static class UserContextHelper
    {
        /// <summary>
        /// Extracts the current user ID from HttpContext claims.
        /// </summary>
        public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User is not authenticated or UserId claim is missing.");
            }
            return userId;
        }

        /// <summary>
        /// Safely attempts to extract the current user ID from HttpContext claims.
        /// </summary>
        public static string? TryGetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
