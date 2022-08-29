using Microsoft.EntityFrameworkCore;

using Vulns.Core;

namespace Vulns.Infrastructure;

internal static class WeaknessModelConfiguration
{
    internal static void ConfigureWeaknessModel(this ModelBuilder modelBuilder)
    {
        // Modeling Weakness
        var builder = modelBuilder.Entity<Weakness>();
        builder.HasMany<Vulnerability>(w => w.Vulnerabilities).WithMany(v => v.Weaknesses);
        builder.HasOne<Weakness>(w => w.Parent).WithMany(w => w.Children);
        builder.HasMany<WeaknessConsequence>(w => w.Consequences);
        builder.HasMany<WeaknessDetection>(w => w.DetectionMethods);

        builder
            .Property(w => w.Type)
            .HasConversion(val => val.ToString(), str => WeaknessType.FromName(str, false));

        builder
            .Property(w => w.ExploitationLikelihood)
            .HasConversion(val => val.ToString(), str => WeaknessExploitationLikelihood.FromName(str, false));

        builder
            .Property(w => w.AffectedResources)
            .IsRequired(false)
            .HasConversion(
                val => val.Any() ? string.Join(", ", val.Select(_ => _.Name)) : null,
                str => WeaknessAffectedResources.FromName(str, false, true).ToList());

        builder.OwnsMany(w => w.Platforms).WithOwner();

        // Modeling WeaknessConsequence
        modelBuilder.Entity<WeaknessConsequence>()
            .Property(wc => wc.Impact)
            .IsRequired(false)
            .HasConversion(
                val => val.Any() ? string.Join(", ", val.Select(_ => _.Name)) : null,
                str => WeaknessConsequenceImpact.FromName(str, false, true).ToList());

        modelBuilder.Entity<WeaknessConsequence>()
            .Property(wc => wc.Scope)
            .IsRequired(false)
            .HasConversion(
                val => val.Any() ? string.Join(", ", val.Select(_ => _.Name)) : null,
                str => WeaknessConsequenceScope.FromName(str, false, true).ToList());

        modelBuilder.Entity<WeaknessConsequence>()
            .Property(wc => wc.Likelihood)
            .HasConversion(val => val.ToString(), str => WeaknessConsequenceLikelihood.FromName(str, false));

        // Modeling WeaknessDetection
        modelBuilder.Entity<WeaknessDetection>()
            .Property(wc => wc.Effectiveness)
            .HasConversion(val => val.ToString(), str => WeaknessDetectionEffectiveness.FromName(str, false));

        modelBuilder.Entity<WeaknessDetection>()
            .Property(wc => wc.Method)
            .HasConversion(val => val.ToString(), str => WeaknessDetectionMethod.FromName(str, false));
    }
}