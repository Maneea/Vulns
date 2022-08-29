//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// This code was generated by XmlSchemaClassGenerator version 2.0.631.0 using the following command:
// xscgen cwe_schema_latest.xsd --namespace=http://cwe.mitre.org/cwe-6=Vulns.Fetchers.Dtos --output=./out/ --disableComments --separateFiles --uniqueTypeNames --netCore --verbose --collectionType=System.Collections.Generic.IList`1 --collectionImplementationType=System.Collections.Generic.List`1 --generatedCodeAttribute=false --description=false --disableComments
namespace Vulns.Jobs.Weaknesses
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.631.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("DetectionMethodEnumeration", Namespace="http://cwe.mitre.org/cwe-6")]
    public enum MitreCweDetectionMethodEnumeration
    {
        
        [System.Xml.Serialization.XmlEnumAttribute("Automated Analysis")]
        Automated_Analysis,
        
        [System.Xml.Serialization.XmlEnumAttribute("Automated Dynamic Analysis")]
        Automated_Dynamic_Analysis,
        
        [System.Xml.Serialization.XmlEnumAttribute("Automated Static Analysis")]
        Automated_Static_Analysis,
        
        [System.Xml.Serialization.XmlEnumAttribute("Automated Static Analysis - Source Code")]
        Automated_Static_Analysis_Source_Code,
        
        [System.Xml.Serialization.XmlEnumAttribute("Automated Static Analysis - Binary or Bytecode")]
        Automated_Static_Analysis_Binary_Or_Bytecode,
        
        Fuzzing,
        
        [System.Xml.Serialization.XmlEnumAttribute("Manual Analysis")]
        Manual_Analysis,
        
        [System.Xml.Serialization.XmlEnumAttribute("Manual Dynamic Analysis")]
        Manual_Dynamic_Analysis,
        
        [System.Xml.Serialization.XmlEnumAttribute("Manual Static Analysis")]
        Manual_Static_Analysis,
        
        [System.Xml.Serialization.XmlEnumAttribute("Manual Static Analysis - Source Code")]
        Manual_Static_Analysis_Source_Code,
        
        [System.Xml.Serialization.XmlEnumAttribute("Manual Static Analysis - Binary or Bytecode")]
        Manual_Static_Analysis_Binary_Or_Bytecode,
        
        [System.Xml.Serialization.XmlEnumAttribute("White Box")]
        White_Box,
        
        [System.Xml.Serialization.XmlEnumAttribute("Black Box")]
        Black_Box,
        
        [System.Xml.Serialization.XmlEnumAttribute("Architecture or Design Review")]
        Architecture_Or_Design_Review,
        
        [System.Xml.Serialization.XmlEnumAttribute("Dynamic Analysis with Manual Results Interpretation")]
        Dynamic_Analysis_With_Manual_Results_Interpretation,
        
        [System.Xml.Serialization.XmlEnumAttribute("Dynamic Analysis with Automated Results Interpretation")]
        Dynamic_Analysis_With_Automated_Results_Interpretation,
        
        [System.Xml.Serialization.XmlEnumAttribute("Formal Verification")]
        Formal_Verification,
        
        [System.Xml.Serialization.XmlEnumAttribute("Simulation / Emulation")]
        Simulation_Slash_Emulation,
        
        Other,
    }
}