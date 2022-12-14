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
    [System.Xml.Serialization.XmlTypeAttribute("TechnicalImpactEnumeration", Namespace="http://cwe.mitre.org/cwe-6")]
    public enum MitreCweTechnicalImpactEnumeration
    {
        
        [System.Xml.Serialization.XmlEnumAttribute("Modify Memory")]
        Modify_Memory,
        
        [System.Xml.Serialization.XmlEnumAttribute("Read Memory")]
        Read_Memory,
        
        [System.Xml.Serialization.XmlEnumAttribute("Modify Files or Directories")]
        Modify_Files_Or_Directories,
        
        [System.Xml.Serialization.XmlEnumAttribute("Read Files or Directories")]
        Read_Files_Or_Directories,
        
        [System.Xml.Serialization.XmlEnumAttribute("Modify Application Data")]
        Modify_Application_Data,
        
        [System.Xml.Serialization.XmlEnumAttribute("Read Application Data")]
        Read_Application_Data,
        
        [System.Xml.Serialization.XmlEnumAttribute("DoS: Crash, Exit, or Restart")]
        DoSColon_CrashComma_ExitComma_Or_Restart,
        
        [System.Xml.Serialization.XmlEnumAttribute("DoS: Amplification")]
        DoSColon_Amplification,
        
        [System.Xml.Serialization.XmlEnumAttribute("DoS: Instability")]
        DoSColon_Instability,
        
        [System.Xml.Serialization.XmlEnumAttribute("DoS: Resource Consumption (CPU)")]
        DoSColon_Resource_Consumption_LeftParenthesisCPURightParenthesis,
        
        [System.Xml.Serialization.XmlEnumAttribute("DoS: Resource Consumption (Memory)")]
        DoSColon_Resource_Consumption_LeftParenthesisMemoryRightParenthesis,
        
        [System.Xml.Serialization.XmlEnumAttribute("DoS: Resource Consumption (Other)")]
        DoSColon_Resource_Consumption_LeftParenthesisOtherRightParenthesis,
        
        [System.Xml.Serialization.XmlEnumAttribute("Execute Unauthorized Code or Commands")]
        Execute_Unauthorized_Code_Or_Commands,
        
        [System.Xml.Serialization.XmlEnumAttribute("Gain Privileges or Assume Identity")]
        Gain_Privileges_Or_Assume_Identity,
        
        [System.Xml.Serialization.XmlEnumAttribute("Bypass Protection Mechanism")]
        Bypass_Protection_Mechanism,
        
        [System.Xml.Serialization.XmlEnumAttribute("Hide Activities")]
        Hide_Activities,
        
        [System.Xml.Serialization.XmlEnumAttribute("Alter Execution Logic")]
        Alter_Execution_Logic,
        
        [System.Xml.Serialization.XmlEnumAttribute("Quality Degradation")]
        Quality_Degradation,
        
        [System.Xml.Serialization.XmlEnumAttribute("Unexpected State")]
        Unexpected_State,
        
        [System.Xml.Serialization.XmlEnumAttribute("Varies by Context")]
        Varies_By_Context,
        
        [System.Xml.Serialization.XmlEnumAttribute("Reduce Maintainability")]
        Reduce_Maintainability,
        
        [System.Xml.Serialization.XmlEnumAttribute("Reduce Performance")]
        Reduce_Performance,
        
        [System.Xml.Serialization.XmlEnumAttribute("Reduce Reliability")]
        Reduce_Reliability,
        
        Other,
    }
}
