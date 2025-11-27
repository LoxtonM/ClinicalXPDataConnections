using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClinicalXPDataConnections.Models
{   

    [Table("ListDocumentsNEW", Schema = "wmfacs_user")]
    public class Document
    {
        [Key]
        public string DocCode { get; set; }
        public string? DocName { get; set; }
        public string? DocGroup { get; set; }
        public bool TemplateInUseNow { get; set; }
        public bool HasAdditionalActions { get; set; }
    }

    [Table("ListDocumentsContent", Schema = "wmfacs_user")]
    public class DocumentsContent
    {
        [Key]
        public int DocContentID { get; set; }
        public string DocCode { get; set; }
        public string LetterTo { get; set; }
        public string LetterFrom { get; set; }
        public string? Para1 { get; set; }
        public string? Para2 { get; set; }
        public string? Para3 { get; set; }
        public string? Para4 { get; set; }
        public string? Para5 { get; set; }
        public string? Para6 { get; set; }
        public string? Para7 { get; set; }
        public string? Para8 { get; set; }
        public string? Para9 { get; set; }
        public string? Para10 { get; set; }
        public string? Para11 { get; set; }
        public string? Para12 { get; set; }
        public string? Para13 { get; set; }
        public string? OurAddress { get; set; }
        public string? DirectLine { get; set; }
        public string? OurEmailAddress { get; set; }
        public string? cc1 { get; set; }
        public string? cc2 { get; set; }
        public bool hasPhenotipsPPQ { get; set; }

    }
     
    [Table("Constants", Schema = "dbo")]
    public class Constant
    {
        [Key]
        public string ConstantCode { get; set; }
        public string ConstantValue { get; set; }
        public string? ConstantValue2 { get; set; }
    }

    [Table("Leaflets", Schema = "dbo")]
    public class Leaflet
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Printable { get; set; }
        public bool ExternalLeaflet { get; set; }
        public string Site { get; set; }
        public bool CancerLeaflet { get; set; }
        public bool GeneralLeaflet { get; set; }
        public bool InUse { get; set; }

    }

    [Table("DocumentKinds", Schema = "dbo")]
    public class DocumentKinds
    {
        [Key]
        public string DocumentKind { get; set; }
        public string DocumentKindDescription { get; set; }
        public int DisplayOrder { get; set; }
    }
}