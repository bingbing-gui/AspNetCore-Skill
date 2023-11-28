using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Authentication.AuthorizationRequirements
{
    public class CustomRequireClaim : IAuthorizationRequirement
    {

        public CustomRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; set; }
    }
    public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomRequireClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(claim => claim.Type == requirement.ClaimType);
            if (hasClaim)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public static class AuthorizationPolicyBuilderExtention
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
           this AuthorizationPolicyBuilder builder,
           string claimType)
        {
            builder.AddRequirements(new CustomRequireClaim(claimType));
            return builder;
        }
    }
}
