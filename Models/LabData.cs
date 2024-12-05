using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicalXPDataConnections.Models
{
    [Table("DISEASE", Schema = "dbo")]
    public class LabDisease
    {
        [Key]
        public string DISEASE_CODE { get; set; }
        public string? DESCRIPTION { get; set; }
    }

    [Table("DNALAB", Schema = "dbo")]
    public class LabDNALab
    {
        [Key]
        public string LABNO { get; set; }
        public int INTID { get; set; }
        public DateTime? DATE_OBTAINED { get; set; }
        public DateTime? DATE_RECEIVED { get; set; }
        public string? SAMPLETYPE { get; set; }
        public string? OTHER_INDICATION_TEXT { get; set; }
    }

    [Table("DNALAB_INDICATION", Schema = "dbo")]
    [Keyless]
    public class LabDNAIndication
    {
        public string LABNO { get; set; }
        public string? INDICATION { get; set; }
        public string? REASON { get; set; }
        public DateTime? DATEREQUESTED { get; set; }
    }

    [Table("DNALAB_REPORT", Schema = "dbo")]
    [Keyless]
    public class LabDNAReport
    {
        public string LABNO { get; set; }
        public string? INDICATION { get; set; }
        public string? REASON { get; set; }
        public Int16? SEQ { get; set; }
        public string? DIAGNOSIS_REPORT { get; set; }
        public string? REPORT_STATUS { get; set; }
        public DateTime? REPORT_DATE { get; set; }
        public string? SUMMARY { get; set; }
        public string? REPORT {  get; set; }
    }

    [Table("LAB", Schema = "dbo")]
    public class LabLab
    {
        [Key]
        public string LABNO { get; set; }
        public int INTID { get; set; }
        public DateTime? DATE_OBTAINED { get; set; }
        public DateTime? DATE_RECEIVED { get; set; }       
        public string? SAMPLETYPE { get; set; }
        public string? INDICATION { get; set; }
        public string? OTHER_INDICATION_TEXT { get; set; }
        public DateTime? REPORT_DATE { get; set; }
        public string? REPORT_STATUS { get; set; }
        public string? REPORT_SUMMARY { get; set; }
        public string? REPORT { get; set; }
        public string? REPORT_STND_TEXT { get; set; }
    }

    [Table("PATIENT", Schema = "dbo")]
    public class LabPatient
    {
        [Key]
        public int INTID { get; set; }
        public string? TITLE { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? ZIP { get; set; }
        public string? SOCIAL_SECURITY { get; set; }
        public DateTime? DOB { get; set; }
    }

    [Table("REF_FAC", Schema = "dbo")]
    public class LabRefFac
    {
        [Key]
        public string FACILITY { get; set; }
    }

    [Table("REFPHYS", Schema = "dbo")]
    public class LabRefPhys
    {
        [Key]
        public string REF_PHYS_CODE { get; set; }
    }

    [Table("STAFF", Schema = "dbo")]
    public class LabStaff
    {
        [Key]
        public string STAFF_CODE { get; set; }
        public string NAME { get; set; }        
    }
    
    [Table("LAB_SENDOUT", Schema = "dbo")]
    [Keyless]
    public class LabSendout
    {
        public string LABNO { get; set; }
        public string TEST { get; set; }
        public Int16 SEQ { get; set; }
    }

    [Keyless]
    public class LabDNALabData
    {
        public string LABNO { get; set; }
        public int INTID { get; set; }
        public string? INDICATION { get; set; }
        public string? REASON { get; set; }
        public Int16? SEQ { get; set; }
        public string? DIAGNOSIS_REPORT { get; set; }
        public string? REPORT_STATUS { get; set; }
        public DateTime? REPORT_DATE { get; set; }
        public DateTime? DATEREQUESTED { get; set; }
    }
}
