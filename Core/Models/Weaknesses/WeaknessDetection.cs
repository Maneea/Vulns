namespace Vulns.Core;
public class WeaknessDetection
{
    public string? Id { get; set; }
    public WeaknessDetectionMethod Method { get; set; } = WeaknessDetectionMethod.Unspecified;
    public WeaknessDetectionEffectiveness Effectiveness { get; set; } = WeaknessDetectionEffectiveness.Unspecified;
    public String? Description { get; set; }
    public String? EffectivenessNotes { get; set; }
}