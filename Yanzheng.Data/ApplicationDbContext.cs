using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yanzheng.Identity;

namespace Yanzheng.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, IdentityRole<long>, long>(options)
{
    const string IdentitySchema = "Identity";

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(entity =>
        {   
            entity.ToTable("Users", IdentitySchema);

            entity.HasIndex(e => e.PublicId).IsUnique();

            builder.Entity<User>().Property(u => u.FirstName);
            builder.Entity<User>().Property(u => u.LastName);
            builder.Entity<User>().Property(u => u.RowVersion).IsRowVersion().IsRequired(false);
        });
        
        builder.Entity<IdentityRole<long>>().ToTable("Roles", IdentitySchema);
        builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims", IdentitySchema);
        builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles", IdentitySchema);
        builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins", IdentitySchema);
        builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims", IdentitySchema);
        builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens", IdentitySchema);
    }
}