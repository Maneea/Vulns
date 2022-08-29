using Microsoft.EntityFrameworkCore;

using Vulns.Core;

namespace Vulns.Infrastructure;

internal static class ProductModelConfiguration
{
    internal static void ConfigureProductModel(this ModelBuilder modelBuilder)
    {
        var builder = modelBuilder.Entity<Product>();
        builder.Property(p => p.Id).HasColumnType("varchar(500)");
        builder.HasMany(p => p.References);
        builder.OwnsOne(p => p.Uri, uri => uri.WithoutPrefix());

        modelBuilder.Entity<ProductReference>()
            .Property(r => r.Tag)
            .HasConversion(val => val.ToString(), str => ProductReferenceTag.FromName(str, false));
    }
}