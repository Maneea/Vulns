
using AutoMapper;

using Vulns.Core;
namespace Vulns.Infrastructure;

internal class WeaknessDocumentMapping : Profile
{
    public WeaknessDocumentMapping()
    {
        // ------------------------------- Entity to Document -------------------------------  //
        CreateMap<Weakness, WeaknessDocument>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Name))
            .ForMember(d => d.ExploitationLikelihood, o => o.MapFrom(s => s.ExploitationLikelihood.Name))
            .ForMember(d => d.AffectedResources, o => o.MapFrom(s => s.AffectedResources.Select(_ => _.Name)))
            .ForMember(d => d.Platforms, o => o.MapFrom(s => s.Platforms.Select(p => p.Name)))
            .ForMember(d => d.DirectParent, o => o.MapFrom(s => s.Parent != null ? s.Parent : null))
            .ForMember(d => d.AllParents, o => o.MapFrom(s => s.AllParents()))
            .ForMember(d => d.DirectChildren, o => o.MapFrom(s => s.Children.Select(w => w.Id)))
            .ForMember(d => d.AllChildren, o => o.MapFrom(s => s.AllChildren()));

        CreateMap<WeaknessDetection, WeaknessDetectionDocument>()
            .ForMember(d => d.Method, o => o.MapFrom(s => s.Method.Name))
            .ForMember(d => d.Effectiveness, o => o.MapFrom(s => s.Effectiveness.Name));

        CreateMap<WeaknessConsequence, WeaknessConsequenceDocument>()
            .ForMember(d => d.Scope, o => o.MapFrom(s => s.Scope.Select(_ => _.Name)))
            .ForMember(d => d.Impact, o => o.MapFrom(s => s.Impact.Select(_ => _.Name)))
            .ForMember(d => d.Likelihood, o => o.MapFrom(s => s.Likelihood.Name));

        // ------------------------------- Document to Entity -------------------------------  //
        CreateMap<WeaknessDocument, Weakness>()
            .ForMember(d => d.Type, o => o.MapFrom(s => WeaknessType.FromName(s.Type!, false)))
            .ForMember(d => d.Platforms, o => o.MapFrom(s => s.Platforms.Select(p => new WeaknessPlatform() { Name = p })))
            .ForMember(d => d.ExploitationLikelihood, o => o.MapFrom(s => WeaknessExploitationLikelihood.FromName(s.ExploitationLikelihood!, false)))
            .ForMember(d => d.AffectedResources, o => o.MapFrom(s => s.AffectedResources.Any() ? WeaknessAffectedResources.FromName(string.Join(", ", s.AffectedResources), false, false) : new List<WeaknessAffectedResources>()));

        CreateMap<WeaknessDetectionDocument, WeaknessDetection>()
            .ForMember(d => d.Method, o => o.MapFrom(s => WeaknessDetectionMethod.FromName(s.Method!, false)))
            .ForMember(d => d.Effectiveness, o => o.MapFrom(s => WeaknessDetectionEffectiveness.FromName(s.Effectiveness!, false)));

        CreateMap<WeaknessConsequenceDocument, WeaknessConsequence>()
            .ForMember(d => d.Likelihood, o => o.MapFrom(s => WeaknessConsequenceLikelihood.FromName(s.Likelihood!, false)))
            .ForMember(d => d.Scope, o => o.MapFrom(s => s.Scope.Any() ? WeaknessConsequenceScope.FromName(string.Join(", ", s.Scope), false, false) : new List<WeaknessConsequenceScope>()))
            .ForMember(d => d.Impact, o => o.MapFrom(s => s.Impact.Any() ? WeaknessConsequenceImpact.FromName(string.Join(", ", s.Impact), false, false) : new List<WeaknessConsequenceImpact>()));
    }
}