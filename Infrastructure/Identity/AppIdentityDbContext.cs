using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
  public class AppIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
  {
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<EMS_Permission> Permissions { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);

   
      builder.Entity<ApplicationUser>(b =>
      {
        b.Property(e=>e.Id).ValueGeneratedOnAdd();
        b.HasIndex(e => e.UserName).IsUnique();
        b.HasIndex(e => e.Email).IsUnique();
        b.HasIndex(e => e.PhoneNumber).IsUnique();

        // Each User can have many UserClaims
        b.HasMany(e => e.Claims)
          .WithOne()
          .HasForeignKey(uc => uc.UserId)
          .IsRequired();

        // Each User can have many UserLogins
        b.HasMany(e => e.Logins)
            .WithOne()
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        // Each User can have many UserTokens
        b.HasMany(e => e.Tokens)
            .WithOne()
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        // Each User can have many entries in the UserRole join table
        b.HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
      });


      builder.Entity<ApplicationRole>(role =>
      {
        role.Property(e => e.Id).ValueGeneratedOnAdd();

        // Each Role can have many entries in the UserRole join table
        role.HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
      });


      builder.Entity<EMS_Permission>(table =>
      {
        table.HasKey(x => x.ID);
        table.HasOne(x => x.ApplicationUserRole)
        .WithMany(x => x.Permissions)
        .HasForeignKey(x => x.RoleId)
        .HasPrincipalKey(x => x.RoleId)//<<== here is core code to let foreign key userId point to User.Id.
        .OnDelete(DeleteBehavior.Cascade);
      });


      //builder.Entity<ApplicationUserRole>(userRole =>
      //{
      //  userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

      //  userRole.HasOne(ur => ur.Role)
      //      .WithMany(r => r.UserRoles)
      //      .HasForeignKey(ur => ur.RoleId)
      //      .IsRequired();

      //  userRole.HasOne(ur => ur.User)
      //      .WithMany(r => r.UserRoles)
      //      .HasForeignKey(ur => ur.UserId)
      //      .IsRequired();
      //});

      //builder.Entity<IdentityUserLogin<string>>(b =>
      //{
      //  b.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
      //});

      //builder.Entity<IdentityUserToken<string>>(b =>
      //{
      //  b.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name});
      //});
    }
  }
}
