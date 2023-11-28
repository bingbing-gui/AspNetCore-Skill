using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.IdentityJWT.API.Model
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {

        }
    }
}
