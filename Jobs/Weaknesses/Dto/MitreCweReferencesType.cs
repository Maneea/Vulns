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
    [System.Xml.Serialization.XmlTypeAttribute("ReferencesType", Namespace="http://cwe.mitre.org/cwe-6")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MitreCweReferencesType
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.Generic.List<MitreCweMitreCweReferencesTypeReference> _reference;
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public System.Collections.Generic.List<MitreCweMitreCweReferencesTypeReference> Reference
        {
            get
            {
                return this._reference;
            }
            private set
            {
                this._reference = value;
            }
        }
        
        public MitreCweReferencesType()
        {
            this._reference = new System.Collections.Generic.List<MitreCweMitreCweReferencesTypeReference>();
        }
    }
}
