using Vulns.Core;

namespace Vulns.Web;
public static class DtoConstants
{
    public static readonly string Unspecified = nameof(Unspecified);
    public static readonly string Unknown = nameof(Unknown);
    public static readonly string NotApplicable = nameof(NotApplicable).Humanize();
    public static readonly string NotFound = nameof(NotFound).Humanize();
    public static readonly string NotAcceptable = nameof(NotAcceptable).Humanize();
}