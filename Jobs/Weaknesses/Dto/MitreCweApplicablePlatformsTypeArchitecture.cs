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
    [System.Xml.Serialization.XmlTypeAttribute("MitreCweApplicablePlatformsTypeArchitecture", Namespace="http://cwe.mitre.org/cwe-6", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MitreCweMitreCweApplicablePlatformsTypeArchitecture
    {
        
        [System.Xml.Serialization.XmlAttributeAttribute("Name")]
        public MitreCweArchitectureNameEnumeration Name { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NameSpecified { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute("Class")]
        public MitreCweArchitectureClassEnumeration Class { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClassSpecified { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlAttributeAttribute("Prevalence")]
        public MitreCwePrevalenceEnumeration Prevalence { get; set; }
    }
}
