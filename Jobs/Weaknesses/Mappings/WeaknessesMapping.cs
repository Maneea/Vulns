using AutoMapper;

using Vulns.Core;

namespace Vulns.Jobs.Weaknesses;

internal class WeaknessesMapping : Profile
{
    public WeaknessesMapping()
    {
        CreateMap<MitreCweWeaknessType, Weakness>()
            .ForMember(d => d.Id, o => o.MapFrom(s => $"CWE-{s.ID}"))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.Content_History.Submission.Submission_Date))
            .ForMember(d => d.ModifiedAt, o => o.ConvertUsing(new ModificationDateConverter(), s => s.Content_History.Modification))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Type, o => o.MapFrom(s => WeaknessType.FromValue((int)s.Abstraction + 1)))
            .ForMember(d => d.ExploitationLikelihood, o => o.ConvertUsing(new ExploitationLikelihoodConverter(), s => s))
            .ForMember(d => d.AffectedResources, o => o.ConvertUsing(new AffectedResourcesConverter(), s => s))
            .ForMember(d => d.Platforms, o => o.ConvertUsing(new PlatformsConverter(), s => s.Applicable_Platforms))
            .ForMember(d => d.Parent, o => o.ConvertUsing(new ParentConverter(), s => s))
            .ForMember(d => d.Consequences, o => o.MapFrom(s => s.Common_Consequences))
            .ForMember(d => d.DetectionMethods, o => o.MapFrom(s => s.Detection_Methods))
            .AfterMap((s, d, ctx) =>
            {
                foreach (var cons in d.Consequences)
                    cons.Id = $"{d.Id}-{cons.Scope.Aggregate(0, (r, v) => r + v.Value)}-{cons.Likelihood.Value}-{cons.Impact.Aggregate(0, (r, v) => r + v.Value)}";
                foreach (var det in d.DetectionMethods) det.Id = $"{d.Id}-{det.Method.Value}-{det.Effectiveness.Value}";
                foreach (var plt in d.Platforms) plt.Id = $"{d.Id}-{plt.Name}";

                List<WeaknessConsequence> newCons = new();
                foreach (var cons in d.Consequences)
                {
                    var tmp = newCons.FirstOrDefault(c => c.Id == cons.Id);
                    if (tmp == null) newCons.Add(cons);
                    else tmp.Note += $"\n{cons.Note}";
                }
                d.Consequences = newCons;

                List<WeaknessDetection> newDets = new();
                foreach (var det in d.DetectionMethods)
                {
                    var tmp = newDets.FirstOrDefault(c => c.Id == det.Id);
                    if (tmp == null) newDets.Add(det);
                    else tmp.Description += $"\n{det.Description}";
                }
                d.DetectionMethods = newDets;
            });

        CreateMap<MitreCweMitreCweCommonConsequencesTypeConsequence, WeaknessConsequence>()
            .ForMember(d => d.Scope, o => o.ConvertUsing(new ConsequenceScopeConverter(), s => s.Scope))
            .ForMember(d => d.Likelihood, o => o.ConvertUsing(new ConsequenceLikelihoodConverter(), s => s))
            .ForMember(d => d.Impact, o => o.ConvertUsing(new ConsequenceImpactConverter(), s => s.Impact))
            .ForMember(d => d.Note, o => o.MapFrom(s => string.Join('\n', s.Note.Text)));

        CreateMap<MitreCweMitreCweDetectionMethodsTypeDetection_Method, WeaknessDetection>()
            .ForMember(d => d.Description, o => o.MapFrom(s => string.Join('\n', s.Description.Text)))
            .ForMember(d => d.EffectivenessNotes, o => o.MapFrom(s => string.Join('\n', s.Effectiveness_Notes.Text)))
            .ForMember(d => d.Method, o => o.MapFrom(s => WeaknessDetectionMethod.FromValue((int)s.Method + 1)))
            .ForMember(d => d.Effectiveness, o => o.MapFrom(s => WeaknessDetectionEffectiveness.FromValue((int)s.Effectiveness + 1)));
    }


}

internal class ModificationDateConverter : IValueConverter<List<MitreCweMitreCweContentHistoryTypeModification>, DateTime?>
{
    public DateTime? Convert(List<MitreCweMitreCweContentHistoryTypeModification> source, ResolutionContext context)
    {
        var dates = source.Where(m => m.Modification_DateSpecified).Select(m => m.Modification_Date);
        if (dates.Count() == 0) return null;
        return dates.Max();
    }
}

internal class ExploitationLikelihoodConverter : IValueConverter<MitreCweWeaknessType, WeaknessExploitationLikelihood>
{
    public WeaknessExploitationLikelihood Convert(MitreCweWeaknessType source, ResolutionContext context)
        => source.Likelihood_Of_ExploitSpecified ?
            WeaknessExploitationLikelihood.FromValue((int)source.Likelihood_Of_Exploit + 1) :
            WeaknessExploitationLikelihood.Unknown;
}

internal class ConsequenceLikelihoodConverter : IValueConverter<MitreCweMitreCweCommonConsequencesTypeConsequence, WeaknessConsequenceLikelihood>
{
    public WeaknessConsequenceLikelihood Convert(MitreCweMitreCweCommonConsequencesTypeConsequence source, ResolutionContext context)
        => source.LikelihoodSpecified ?
            WeaknessConsequenceLikelihood.FromValue((int)source.Likelihood + 1) :
            WeaknessConsequenceLikelihood.Unspecified;
}

internal class AffectedResourcesConverter : IValueConverter<MitreCweWeaknessType, ICollection<WeaknessAffectedResources>>
{
    public ICollection<WeaknessAffectedResources> Convert(MitreCweWeaknessType source, ResolutionContext context)
    {
        List<WeaknessAffectedResources> ret = new();
        foreach (var affectedResource in source.Affected_Resources)
            switch (affectedResource)
            {
                case MitreCweResourceEnumeration.CPU: ret.Add(WeaknessAffectedResources.Cpu); break;
                case MitreCweResourceEnumeration.File_Or_Directory: ret.Add(WeaknessAffectedResources.FileOrDirectory); break;
                case MitreCweResourceEnumeration.Memory: ret.Add(WeaknessAffectedResources.Memory); break;
                case MitreCweResourceEnumeration.System_Process: ret.Add(WeaknessAffectedResources.SystemProcess); break;
                case MitreCweResourceEnumeration.Other: ret.Add(WeaknessAffectedResources.Other); break;
            }
        ret = ret.Distinct().ToList();
        return ret;
    }
}

internal class PlatformsConverter : IValueConverter<MitreCweApplicablePlatformsType, ICollection<WeaknessPlatform>>
{
    public ICollection<WeaknessPlatform> Convert(MitreCweApplicablePlatformsType s, ResolutionContext context)
    {
        List<WeaknessPlatform> ret = new();
        if (s == null) return ret;
        if (s.ArchitectureSpecified) foreach (var p in s.Architecture)
                ret.Add(new()
                {
                    Name = p.NameSpecified ? p.Name.GetXmlEnum() : p.ClassSpecified ? p.Class.GetXmlEnum() : null,
                    Prelavence = p.Prevalence.GetXmlEnum(),
                    Type = "Architecture"
                });
        if (s.LanguageSpecified) foreach (var p in s.Language)
                ret.Add(new()
                {
                    Name = p.NameSpecified ? p.Name.GetXmlEnum() : p.ClassSpecified ? p.Class.GetXmlEnum() : null,
                    Prelavence = p.Prevalence.GetXmlEnum(),
                    Type = "Language"
                });
        if (s.Operating_SystemSpecified) foreach (var p in s.Operating_System)
                ret.Add(new()
                {
                    Name = p.NameSpecified ? p.Name.GetXmlEnum() : p.ClassSpecified ? p.Class.GetXmlEnum() : null,
                    Prelavence = p.Prevalence.GetXmlEnum(),
                    Type = "Operating System"
                });
        if (s.TechnologySpecified) foreach (var p in s.Technology)
                ret.Add(new()
                {
                    Name = p.NameSpecified ? p.Name.GetXmlEnum() : p.ClassSpecified ? p.Class.GetXmlEnum() : null,
                    Prelavence = p.Prevalence.GetXmlEnum(),
                    Type = "Technology"
                });
        return ret;
    }
}

internal class ParentConverter : IValueConverter<MitreCweWeaknessType, Weakness?>
{
    public Weakness? Convert(MitreCweWeaknessType source, ResolutionContext context)
    {
        if (!source.Related_WeaknessesSpecified) return null;

        var nature = MitreCweRelatedNatureEnumeration.ChildOf;
        var relations = source.Related_Weaknesses.Where(r => r.View_ID == "1000" && r.Nature == nature);
        if (relations.Where(r => r.OrdinalSpecified && r.Ordinal == MitreCweOrdinalEnumeration.Primary).Any())
            relations = relations.Where(r => r.OrdinalSpecified && r.Ordinal == MitreCweOrdinalEnumeration.Primary);
        return relations.Any() ? new() { Id = $"CWE-{relations.First().CWE_ID}" } : null;
    }
}

internal class ConsequenceScopeConverter : IValueConverter<List<MitreCweScopeEnumeration>, ICollection<WeaknessConsequenceScope>>
{
    public ICollection<WeaknessConsequenceScope> Convert(List<MitreCweScopeEnumeration> source, ResolutionContext context)
    {
        List<WeaknessConsequenceScope> ret = new();
        foreach (var scope in source)
            switch (scope)
            {
                case MitreCweScopeEnumeration.Confidentiality: ret.Add(WeaknessConsequenceScope.Confidentiality); break;
                case MitreCweScopeEnumeration.Integrity: ret.Add(WeaknessConsequenceScope.Integrity); break;
                case MitreCweScopeEnumeration.Availability: ret.Add(WeaknessConsequenceScope.Availability); break;
                case MitreCweScopeEnumeration.Access_Control: ret.Add(WeaknessConsequenceScope.AccessControl); break;
                case MitreCweScopeEnumeration.Accountability: ret.Add(WeaknessConsequenceScope.Accountability); break;
                case MitreCweScopeEnumeration.Authentication: ret.Add(WeaknessConsequenceScope.Authentication); break;
                case MitreCweScopeEnumeration.Authorization: ret.Add(WeaknessConsequenceScope.Authorization); break;
                case MitreCweScopeEnumeration.Non_Repudiation: ret.Add(WeaknessConsequenceScope.NonRepudiation); break;
                case MitreCweScopeEnumeration.Other: ret.Add(WeaknessConsequenceScope.Other); break;
            }
        ret = ret.Distinct().ToList();
        return ret;
    }
}

internal class ConsequenceImpactConverter : IValueConverter<List<MitreCweTechnicalImpactEnumeration>, ICollection<WeaknessConsequenceImpact>>
{
    public ICollection<WeaknessConsequenceImpact> Convert(List<MitreCweTechnicalImpactEnumeration> source, ResolutionContext context)
    {
        List<WeaknessConsequenceImpact> ret = new();
        foreach (var impact in source)
            switch (impact)
            {
                case MitreCweTechnicalImpactEnumeration.Modify_Memory: ret.Add(WeaknessConsequenceImpact.ModifyMemory); break;
                case MitreCweTechnicalImpactEnumeration.Read_Memory: ret.Add(WeaknessConsequenceImpact.ReadMemory); break;
                case MitreCweTechnicalImpactEnumeration.Modify_Files_Or_Directories: ret.Add(WeaknessConsequenceImpact.ModifyFilesOrDirectories); break;
                case MitreCweTechnicalImpactEnumeration.Read_Files_Or_Directories: ret.Add(WeaknessConsequenceImpact.ReadFilesOrDirectories); break;
                case MitreCweTechnicalImpactEnumeration.Modify_Application_Data: ret.Add(WeaknessConsequenceImpact.ModifyApplicationData); break;
                case MitreCweTechnicalImpactEnumeration.Read_Application_Data: ret.Add(WeaknessConsequenceImpact.ReadApplicationData); break;
                case MitreCweTechnicalImpactEnumeration.DoSColon_CrashComma_ExitComma_Or_Restart: ret.Add(WeaknessConsequenceImpact.DosCrashOrExitOrRestart); break;
                case MitreCweTechnicalImpactEnumeration.DoSColon_Amplification: ret.Add(WeaknessConsequenceImpact.DosAmplification); break;
                case MitreCweTechnicalImpactEnumeration.DoSColon_Instability: ret.Add(WeaknessConsequenceImpact.DosInstability); break;
                case MitreCweTechnicalImpactEnumeration.DoSColon_Resource_Consumption_LeftParenthesisCPURightParenthesis: ret.Add(WeaknessConsequenceImpact.DosCpuConsumption); break;
                case MitreCweTechnicalImpactEnumeration.DoSColon_Resource_Consumption_LeftParenthesisMemoryRightParenthesis: ret.Add(WeaknessConsequenceImpact.DosMemoryConsumption); break;
                case MitreCweTechnicalImpactEnumeration.DoSColon_Resource_Consumption_LeftParenthesisOtherRightParenthesis: ret.Add(WeaknessConsequenceImpact.DosOtherResourceConsumption); break;
                case MitreCweTechnicalImpactEnumeration.Execute_Unauthorized_Code_Or_Commands: ret.Add(WeaknessConsequenceImpact.ExecuteUnauthorizedCodeOrCommands); break;
                case MitreCweTechnicalImpactEnumeration.Gain_Privileges_Or_Assume_Identity: ret.Add(WeaknessConsequenceImpact.GainPrivilegesOrAssumeIdentity); break;
                case MitreCweTechnicalImpactEnumeration.Bypass_Protection_Mechanism: ret.Add(WeaknessConsequenceImpact.BypassProtectionMechanism); break;
                case MitreCweTechnicalImpactEnumeration.Hide_Activities: ret.Add(WeaknessConsequenceImpact.HideActivities); break;
                case MitreCweTechnicalImpactEnumeration.Alter_Execution_Logic: ret.Add(WeaknessConsequenceImpact.AlterExecutionLogic); break;
                case MitreCweTechnicalImpactEnumeration.Quality_Degradation: ret.Add(WeaknessConsequenceImpact.QualityDegradation); break;
                case MitreCweTechnicalImpactEnumeration.Unexpected_State: ret.Add(WeaknessConsequenceImpact.UnexpectedState); break;
                case MitreCweTechnicalImpactEnumeration.Varies_By_Context: ret.Add(WeaknessConsequenceImpact.VariesByContext); break;
                case MitreCweTechnicalImpactEnumeration.Reduce_Maintainability: ret.Add(WeaknessConsequenceImpact.ReduceMaintainability); break;
                case MitreCweTechnicalImpactEnumeration.Reduce_Performance: ret.Add(WeaknessConsequenceImpact.ReducePerformance); break;
                case MitreCweTechnicalImpactEnumeration.Reduce_Reliability: ret.Add(WeaknessConsequenceImpact.ReduceReliability); break;
                case MitreCweTechnicalImpactEnumeration.Other: ret.Add(WeaknessConsequenceImpact.Other); break;
            }
        ret = ret.Distinct().ToList();
        return ret;
    }
}