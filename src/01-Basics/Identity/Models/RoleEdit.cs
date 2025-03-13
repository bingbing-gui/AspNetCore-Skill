﻿using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class RoleEdit
    {
        public IdentityRole? Role { get; set; }

        public IEnumerable<AppUser>? Members { get; set; }

        public IEnumerable<AppUser>? NoMembers { get; set; }

    }
}
