using System.Text.Json.Serialization;

namespace Vulns.Core;
[JsonConverter(typeof(SmartEnumNameConverter<WeaknessDetectionMethod, int>))]
public class WeaknessDetectionMethod : SmartEnum<WeaknessDetectionMethod>
{
    public static readonly WeaknessDetectionMethod Unspecified = new(nameof(Unspecified), 0);
    public static readonly WeaknessDetectionMethod AutomatedAnalysis = new("Automated Analysis", 1);
    public static readonly WeaknessDetectionMethod AutomatedDynamicAnalysis = new("Automated Dynamic Analysis", 2);
    public static readonly WeaknessDetectionMethod AutomatedStaticAnalysis = new("Automated Static Analysis", 3);
    public static readonly WeaknessDetectionMethod AutomatedStaticSourceCodeAnalysis = new("Automated Static Source Code Analysis", 4);
    public static readonly WeaknessDetectionMethod AutomatedStaticBinaryOrBytecodeAnalysis = new("Automated Static Binary or Bytecode Analysis", 5);
    public static readonly WeaknessDetectionMethod Fuzzing = new(nameof(Fuzzing), 6);
    public static readonly WeaknessDetectionMethod ManualAnalysis = new("Manual Analysis", 7);
    public static readonly WeaknessDetectionMethod ManualDynamicAnalysis = new("Manual Dynamic Analysis", 8);
    public static readonly WeaknessDetectionMethod ManualStaticAnalysis = new("Manual Static Analysis", 9);
    public static readonly WeaknessDetectionMethod ManualStaticSourceCodeAnalysis = new("Manual Static Source Code Analysis", 10);
    public static readonly WeaknessDetectionMethod ManualStaticBinaryOrBytecodeAnalysis = new("Manual Static Binary or Bytecode Analysis", 11);
    public static readonly WeaknessDetectionMethod WhiteBox = new("White Box", 12);
    public static readonly WeaknessDetectionMethod BlackBox = new("Black Box", 13);
    public static readonly WeaknessDetectionMethod ArchitectureOrDesignReview = new("Architecture or Design Review", 14);
    public static readonly WeaknessDetectionMethod DynamicAnalysisWithManualResultsInterpretation = new("Dynamic Analysis With Manual Results Interpretation", 15);
    public static readonly WeaknessDetectionMethod DynamicAnalysisWithAutomatedResultsInterpretation = new("Dynamic Analysis with Automated Results Interpretation", 16);
    public static readonly WeaknessDetectionMethod FormalVerification = new("Formal Verification", 17);
    public static readonly WeaknessDetectionMethod SimulationAndOrEmulation = new("Simulation and/or Emulation", 18);
    public static readonly WeaknessDetectionMethod Other = new(nameof(Other), 19);

    private WeaknessDetectionMethod(string name, int value) : base(name, value)
    {
    }

    public WeaknessDetectionMethod() : base(nameof(Unspecified), 0) { }
}