using AutoMapper;

using Vulns.Core;

namespace Vulns.Jobs.Vulnerabilities;
public class VulnerabilitiesMapping : Profile
{
    public VulnerabilitiesMapping()
    {
        CreateMap<NvdCveItem, Vulnerability>()
            .ForMember(d => d.Id, o => o.MapFrom(s
                => $"CVE-{s.Cve.CveMetadata.Id.Substring(4, 4)}-{s.Cve.CveMetadata.Id.Split('-', StringSplitOptions.None)[2].PadLeft(4, '0')}"))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Cve.Description.Data.FirstOrDefault(new NvdCveLanguageString("", "")).Value))
            .ForMember(d => d.CreatedAt, o => o.ConvertUsing(new DateConverter(), s => s.PublishedDate))
            .ForMember(d => d.ModifiedAt, o => o.ConvertUsing(new NullableDateConverter(), s => s.LastModifiedDate))
            .ForMember(d => d.Weaknesses, o => o.ConvertUsing(new WeaknessConverter(), s => s.Cve.ProblemType))
            .ForMember(d => d.VulnerableProducts, o => o.ConvertUsing(new ProductsConverter(), s => s.Configurations))
            .ForMember(d => d.IssuerEmail, o => o.MapFrom(s => s.Cve.CveMetadata.Assigner))
            .ForMember(d => d.Severity, o => o.MapFrom(s => s.Impact.BaseMetricV2))
            .ForMember(d => d.References, o => o.ConvertUsing(new ReferencesConverter(), s => s.Cve.References))
            .ForMember(d => d.ConfidentialityImpact, o => o.ConvertUsing(new ConfidentialityImpactConverter(), s => s.Impact.BaseMetricV2))
            .ForMember(d => d.IntegrityImpact, o => o.ConvertUsing(new IntegrityImpactConverter(), s => s.Impact.BaseMetricV2))
            .ForMember(d => d.AvailabilityImpact, o => o.ConvertUsing(new AvailabilityImpactConverter(), s => s.Impact.BaseMetricV2))
            .ForMember(d => d.AttackVector, o => o.ConvertUsing(new AttackVectorConverter(), s => s.Impact.BaseMetricV2))
            .ForMember(d => d.AttackComplexity, o => o.ConvertUsing(new AttackComplexityConverter(), s => s.Impact.BaseMetricV2))
            .ForMember(d => d.RequiredAuthentication, o => o.ConvertUsing(new RequiredAuthenticationConverter(), s => s.Impact.BaseMetricV2))
            .AfterMap((s, d, ctx) =>
            {
                if (d.References != null && d.References.Any())
                {
                    foreach (var reference in d.References)
                        reference.Id = $"{d.Id}-{reference.Tags.Aggregate(0, (r, v) => r + v.Value)}-{reference.Url.GetHashCode()}";
                    d.References = d.References.DistinctBy(_ => _.Id).ToList();
                }
            });


        // Severity (CVSS v2) Mappings
        CreateMap<NvdCveCvss2, VulnerabilitySeverity>();
        CreateMap<NvdCveBaseMetricV2, VulnerabilitySeverity>()
            .ForMember(d => d.Level, o => o.ConvertUsing(new SeverityLevelConverter(), s => s.Severity))
            .IncludeMembers(s => s.CvssV2);
    }

    internal class ConfidentialityImpactConverter : IValueConverter<NvdCveBaseMetricV2, VulnerabilityImpact>
    {
        public VulnerabilityImpact Convert(NvdCveBaseMetricV2 source, ResolutionContext context)
            => source == null || source.CvssV2 == null ?
                VulnerabilityImpact.Unspecified :
                VulnerabilityImpact.FromValue((int)source.CvssV2.ConfidentialityImpact);
    }

    internal class IntegrityImpactConverter : IValueConverter<NvdCveBaseMetricV2, VulnerabilityImpact>
    {
        public VulnerabilityImpact Convert(NvdCveBaseMetricV2 source, ResolutionContext context)
            => source == null || source.CvssV2 == null ?
                VulnerabilityImpact.Unspecified :
                VulnerabilityImpact.FromValue((int)source.CvssV2.IntegrityImpact);
    }

    internal class AvailabilityImpactConverter : IValueConverter<NvdCveBaseMetricV2, VulnerabilityImpact>
    {
        public VulnerabilityImpact Convert(NvdCveBaseMetricV2 source, ResolutionContext context)
            => source == null || source.CvssV2 == null ?
                VulnerabilityImpact.Unspecified :
                VulnerabilityImpact.FromValue((int)source.CvssV2.AvailabilityImpact);
    }

    internal class AttackComplexityConverter : IValueConverter<NvdCveBaseMetricV2, VulnerabilityAttackComplexity>
    {
        public VulnerabilityAttackComplexity Convert(NvdCveBaseMetricV2 source, ResolutionContext context)
            => source == null || source.CvssV2 == null ?
                VulnerabilityAttackComplexity.Unspecified :
                VulnerabilityAttackComplexity.FromValue((int)source.CvssV2.AccessComplexity);
    }

    internal class AttackVectorConverter : IValueConverter<NvdCveBaseMetricV2, VulnerabilityAttackVector>
    {
        public VulnerabilityAttackVector Convert(NvdCveBaseMetricV2 source, ResolutionContext context)
            => source == null || source.CvssV2 == null ?
                VulnerabilityAttackVector.Unspecified :
                VulnerabilityAttackVector.FromValue((int)source.CvssV2.AccessVector);
    }

    internal class RequiredAuthenticationConverter : IValueConverter<NvdCveBaseMetricV2, VulnerabilityRequiredAuthentication>
    {
        public VulnerabilityRequiredAuthentication Convert(NvdCveBaseMetricV2 source, ResolutionContext context)
            => source == null || source.CvssV2 == null ?
                VulnerabilityRequiredAuthentication.Unspecified :
                VulnerabilityRequiredAuthentication.FromValue((int)source.CvssV2.Authentication);
    }

    internal class SeverityLevelConverter : IValueConverter<String, VulnerabilitySeverityLevel>
    {
        public VulnerabilitySeverityLevel Convert(String source, ResolutionContext context)
        {
            switch (source)
            {
                case "HIGH": return VulnerabilitySeverityLevel.High;
                case "MEDIUM": return VulnerabilitySeverityLevel.Medium;
                case "LOW": return VulnerabilitySeverityLevel.Low;
                default: return VulnerabilitySeverityLevel.Unspecified;
            }
        }
    }

    internal class DateConverter : IValueConverter<String, DateTime>
    {
        public DateTime Convert(String source, ResolutionContext context)
        {
            DateTime ret = new DateTime();
            DateTime.TryParse(source, out ret);
            return ret;
        }
    }

    internal class NullableDateConverter : IValueConverter<String, DateTime?>
    {
        public DateTime? Convert(String source, ResolutionContext context)
        {
            DateTime? ret = null;
            DateTime tmp = default;
            if (DateTime.TryParse(source, out tmp)) ret = tmp;
            return ret;
        }
    }

    internal class ProductsConverter : IValueConverter<NvdCveApplicabilityStatements, IList<Product>>
    {
        public IList<Product> Convert(NvdCveApplicabilityStatements source, ResolutionContext context)
        {
            IEnumerable<Product> pe = new List<Product>();
            foreach (var node in source.Nodes) pe = pe.Concat(Extract(node));
            return Deduplicate(pe.ToList());
        }

        private IList<Product> Extract(NvdCveApplicabilityStatementNode node)
        {
            IEnumerable<Product> pe = new List<Product>();
            IList<Product> pl = new List<Product>();
            foreach (var match in node.CpeMatch)
                if (match.Vulnerable)
                    pl.Add(new(match.Cpe23Uri));

            pe = pl;
            foreach (var child in node.Children)
                pe = pe.Concat(Extract(child));

            return pe.ToList();
        }

        private IList<Product> Deduplicate(List<Product> products)
        {
            for (int i = 0; i < products.Count; i++)
                for (int j = i + 1; j < products.Count; j++)
                    if (products[i].Equals(products[j]))
                    {
                        products.RemoveAt(j);
                        j--;
                    }
            return products;
        }
    }

    internal class WeaknessConverter : IValueConverter<NvdCveProblemType, IList<Weakness>>
    {
        public IList<Weakness> Convert(NvdCveProblemType source, ResolutionContext context)
        {
            int tmp;
            IList<Weakness> ret = new List<Weakness>();
            foreach (var problemType in source.ProblemTypeData)
                foreach (var lang_String in problemType.Description)
                    if (!lang_String.Value.StartsWith("NVD-CWE") && int.TryParse(lang_String.Value.Substring(4), out tmp))
                        ret.Add(new() { Id = $"CWE-{tmp}" });
            return ret;
        }
    }

    internal class ReferencesConverter : IValueConverter<NvdCveReferences, List<VulnerabilityReference>>
    {
        public List<VulnerabilityReference> Convert(NvdCveReferences source, ResolutionContext context)
        {
            List<VulnerabilityReference> ret = new();
            foreach (var reference in source.References)
                ret.Add(new(reference.Name, reference.Url, StringsToTags(reference.Tags)));
            return ret;
        }

        public ICollection<VulnerabilityReferenceTag> StringsToTags(IList<string> tags)
        {
            List<VulnerabilityReferenceTag> ret = new();
            foreach (var tag in tags)
                switch (tag)
                {
                    case "Broken Link": ret.Add(VulnerabilityReferenceTag.BrokenLink); break;
                    case "Exploit": ret.Add(VulnerabilityReferenceTag.Exploit); break;
                    case "Issue Tracking": ret.Add(VulnerabilityReferenceTag.IssueTracking); break;
                    case "Mailing List": ret.Add(VulnerabilityReferenceTag.MailingList); break;
                    case "Mitigation": ret.Add(VulnerabilityReferenceTag.Mitigation); break;
                    case "Not Applicable": ret.Add(VulnerabilityReferenceTag.NotApplicable); break;
                    case "Patch": ret.Add(VulnerabilityReferenceTag.Patch); break;
                    case "Permissions Required": ret.Add(VulnerabilityReferenceTag.PermissionsRequired); break;
                    case "Press/Media Coverage": ret.Add(VulnerabilityReferenceTag.Press); break;
                    case "Product": ret.Add(VulnerabilityReferenceTag.Product); break;
                    case "Release Notes": ret.Add(VulnerabilityReferenceTag.ReleaseNotes); break;
                    case "Technical Description": ret.Add(VulnerabilityReferenceTag.TechnicalDescription); break;
                    case "Third Party Advisory": ret.Add(VulnerabilityReferenceTag.ThirdPartyAdvisory); break;
                    case "Tool Signature": ret.Add(VulnerabilityReferenceTag.ToolSignature); break;
                    case "URL Repurposed": ret.Add(VulnerabilityReferenceTag.URLRepurposed); break;
                    case "US Government Resource": ret.Add(VulnerabilityReferenceTag.USGovernmentResource); break;
                    case "VDB Entry": ret.Add(VulnerabilityReferenceTag.VDBEntry); break;
                    case "Vendor Advisory": ret.Add(VulnerabilityReferenceTag.VendorAdvisory); break;
                }
            ret = ret.Distinct().ToList();
            return ret;
        }
    }

}