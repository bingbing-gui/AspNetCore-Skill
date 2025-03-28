using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Integrated.Azure.AI.CustomPolicy
{
    public class AllowUsersHandler : AuthorizationHandler<AllowUserPolicy>
    {
        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            return base.HandleAsync(context);
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowUserPolicy requirement)
        {
            if (requirement.AllowUsers.Any(user => user.Equals(context.User.Identity?.Name, StringComparison.OrdinalIgnoreCase)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
