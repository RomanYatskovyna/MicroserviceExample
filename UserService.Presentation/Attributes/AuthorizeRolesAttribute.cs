using Microsoft.AspNetCore.Authorization;

namespace UserService.Presentation.Attributes;

public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}