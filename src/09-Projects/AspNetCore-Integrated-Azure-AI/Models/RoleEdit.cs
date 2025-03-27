using Microsoft.AspNetCore.Identity;

namespace AspNetCore.Integrated.Azure.AI.Models
{
    public class RoleEdit
    {
        public Role? Role { get; set; }

        public IEnumerable<AppUser>? Members { get; set; }

        public IEnumerable<AppUser>? NoMembers { get; set; }

    }
}
