using System.Security.Claims;

namespace Simbir.GO.Api.Helpers
{
    public static class UserClaimsHelper
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier);

            return int.Parse(claim.Value);
        }

    }
}
