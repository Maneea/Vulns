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
    [System.Xml.Serialization.XmlTypeAttribute("MitreCweTaxonomyMappingsTypeTaxonomy_Mapping", Namespace="http://cwe.mitre.org/cwe-6", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MitreCweMitreCweTaxonomyMappingsTypeTaxonomy_Mapping
    {
        
        [System.Xml.Serialization.XmlElementAttribute("Entry_ID")]
        public string Entry_ID { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("Entry_Name")]
        public string Entry_Name { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("Mapping_Fit")]
        public MitreCweTaxonomyMappingFitEnumeration Mapping_Fit { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Mapping_FitSpecified { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlAttributeAttribute("Taxonomy_Name")]
        public MitreCweTaxonomyNameEnumeration Taxonomy_Name { get; set; }
    }
}
