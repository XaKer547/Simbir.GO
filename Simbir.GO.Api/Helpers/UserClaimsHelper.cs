using System.Security.Claims;

namespace Simbir.GO.Api.Helpers
{
    public static class UserClaimsHelper
    {
        public static long GetId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier);

            return long.Parse(claim.Value);
        }
    }
}
