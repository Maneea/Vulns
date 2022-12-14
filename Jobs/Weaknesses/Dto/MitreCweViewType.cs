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
    [System.Xml.Serialization.XmlTypeAttribute("ViewType", Namespace="http://cwe.mitre.org/cwe-6")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MitreCweViewType
    {
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Objective")]
        public MitreCweStructuredTextType Objective { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.Generic.List<MitreCweMitreCweAudienceTypeStakeholder> _audience;
        
        [System.Xml.Serialization.XmlArrayAttribute("Audience")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Stakeholder", Namespace="http://cwe.mitre.org/cwe-6")]
        public System.Collections.Generic.List<MitreCweMitreCweAudienceTypeStakeholder> Audience
        {
            get
            {
                return this._audience;
            }
            private set
            {
                this._audience = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AudienceSpecified
        {
            get
            {
                return (this.Audience.Count != 0);
            }
        }
        
        public MitreCweViewType()
        {
            this._audience = new System.Collections.Generic.List<MitreCweMitreCweAudienceTypeStakeholder>();
            this._references = new System.Collections.Generic.List<MitreCweMitreCweReferencesTypeReference>();
            this._notes = new System.Collections.Generic.List<MitreCweMitreCweNotesTypeNote>();
        }
        
        [System.Xml.Serialization.XmlElementAttribute("Members")]
        public MitreCweRelationshipsType Members { get; set; }
        
        [System.Xml.Serialization.XmlElementAttribute("Filter")]
        public string Filter { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.Generic.List<MitreCweMitreCweReferencesTypeReference> _references;
        
        [System.Xml.Serialization.XmlArrayAttribute("References")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Reference", Namespace="http://cwe.mitre.org/cwe-6")]
        public System.Collections.Generic.List<MitreCweMitreCweReferencesTypeReference> References
        {
            get
            {
                return this._references;
            }
            private set
            {
                this._references = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReferencesSpecified
        {
            get
            {
                return (this.References.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.Generic.List<MitreCweMitreCweNotesTypeNote> _notes;
        
        [System.Xml.Serialization.XmlArrayAttribute("Notes")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Note", Namespace="http://cwe.mitre.org/cwe-6")]
        public System.Collections.Generic.List<MitreCweMitreCweNotesTypeNote> Notes
        {
            get
            {
                return this._notes;
            }
            private set
            {
                this._notes = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NotesSpecified
        {
            get
            {
                return (this.Notes.Count != 0);
            }
        }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Content_History")]
        public MitreCweContentHistoryType Content_History { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string ID { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlAttributeAttribute("Name")]
        public string Name { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlAttributeAttribute("Type")]
        public MitreCweMitreCweViewTypeEnumeration Type { get; set; }
        
        [System.ComponentModel.DataAnnotations.RequiredAttribute()]
        [System.Xml.Serialization.XmlAttributeAttribute("Status")]
        public MitreCweStatusEnumeration Status { get; set; }
    }
}
