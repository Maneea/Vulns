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
    [System.Xml.Serialization.XmlTypeAttribute("NoteTypeEnumeration", Namespace="http://cwe.mitre.org/cwe-6")]
    public enum MitreCweNoteTypeEnumeration
    {
        
        [System.Xml.Serialization.XmlEnumAttribute("Applicable Platform")]
        Applicable_Platform,
        
        Maintenance,
        
        Relationship,
        
        [System.Xml.Serialization.XmlEnumAttribute("Research Gap")]
        Research_Gap,
        
        Terminology,
        
        Theoretical,
        
        Other,
    }
}
