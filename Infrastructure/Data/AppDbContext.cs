using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Vulns.Core;

namespace Vulns.Infrastructure;

public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Vulnerability> Vulnerability => Set<Vulnerability>();
    public DbSet<Product> Product => Set<Product>();
    public DbSet<Weakness> Weakness => Set<Weakness>();
    public DbSet<Issuer> CveNumberingAuthority => Set<Issuer>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Called when creating the very first instance of this class. Never called again.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseGuidCollation(string.Empty);
        modelBuilder.ConfigureVulnerabilityModel();
        modelBuilder.ConfigureProductModel();
        modelBuilder.ConfigureWeaknessModel();
        base.OnModelCreating(modelBuilder);
    }

}

public static class OwnedPropertiesExtensions
{
    public static OwnedNavigationBuilder<T, R> WithoutPrefix<T, R>(this OwnedNavigationBuilder<T, R> builder) where T : class where R : class
    {
        foreach (var p in builder.OwnedEntityType.GetProperties())
            if (!p.Name.EndsWith("Id"))
                builder.Property(p.Name).HasColumnName(p.Name);
        return builder;
    }
}