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
    [System.Xml.Serialization.XmlTypeAttribute("StructuredCodeType", Namespace="http://cwe.mitre.org/cwe-6")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MitreCweStructuredCodeType
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.Generic.List<System.Xml.XmlElement> _any;
        
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Collections.Generic.List<System.Xml.XmlElement> Any
        {
            get
            {
                return this._any;
            }
            private set
            {
                this._any = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AnySpecified
        {
            get
            {
                return (this.Any.Count != 0);
            }
        }
        
        public MitreCweStructuredCodeType()
        {
            this._any = new System.Collections.Generic.List<System.Xml.XmlElement>();
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute("Language")]
        public MitreCweLanguageNameEnumeration Language { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LanguageSpecified { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlAttributeAttribute("Nature")]
        public MitreCweMitreCweStructuredCodeTypeNature Nature { get; set; }
        
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text { get; set; }
    }
}
