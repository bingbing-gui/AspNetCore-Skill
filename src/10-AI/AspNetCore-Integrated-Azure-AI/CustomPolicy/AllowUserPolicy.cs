using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Integrated.Azure.AI.CustomPolicy
{
public class AllowUserPolicy : IAuthorizationRequirement
{
    public string[] AllowUsers { get; set; }

    public AllowUserPolicy(params string[] allowUsers)
    {
        AllowUsers=allowUsers;
    }
}
}
