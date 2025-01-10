using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicalXPDataConnections.Models
{
    [Table("ViewPatientPathwayAll", Schema = "dbo")] //Patient pathway overview
    [Keyless]
    public class PatientPathway
    {
        public int MPI { get; set; }
        public string cgu_no { get; set; }
        public string firstname { get; set; }        
        public string lastname { get; set; }
        public DateTime REFERRAL_DATE { get; set; }
        public int Triaged { get; set; }
        public int AppointmentBooked { get; set; }
        public int Seen { get; set; }
        public int LetterDictated { get; set; }
        public int LetterPrinted { get; set; }
        public int ReviewPlanned { get; set; }
        public string ClockStatus { get; set; }
        public string? PATHWAY { get; set; }
        public int? ToBeSeenByGC { get; set; }
        public int? ToBeSeenByCons { get; set; }
    }

    [Table("ViewPatientDemographicDetails", Schema="dbo")] //Patient demographic data
    public class Patient
    {
        [Key]
        public int MPI { get; set; }
        public int INTID { get; set; }
        public int WMFACSID { get; set; }
        public string? Title { get; set; }
        [Display(Name ="Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [Display(Name = "Date of Death")]
        [DataType(DataType.Date)]
        public DateTime? DECEASED_DATE { get; set; }
        public Int16 DECEASED {  get; set; }
        [Display(Name = "Forename")]
        public string FIRSTNAME { get; set; }
        public string? PtForename2 { get; set; }
        public string? PtSurnameBirth { get; set; }
        public string? PREVIOUS_NAME { get; set; }
        [Display(Name = "Surname")]
        public string LASTNAME { get; set; }
        [Display(Name = "CGU Number")]
        public string? CGU_No { get; set; }
        public string? PEDNO { get; set; }
        [Display(Name = "NHS Number")]
        public string SOCIAL_SECURITY { get; set; }
        public string SEX { get; set; }
        public string? ADDRESS1 { get; set; }
        public string? ADDRESS2 { get; set; }
        public string? ADDRESS3 { get; set; }
        public string? ADDRESS4 { get; set; }
        public string? POSTCODE { get; set; }
        public string? COUNTRY { get; set; }
        public string? PtAreaCode { get; set; }
        public string? TEL { get; set; }
        public string? WORKTEL { get; set; }
        public string? PtTelMobile { get; set; }               
        public string? EmailCommsConsent { get; set; }
        public string? EmailAddress { get; set; }
        public string? PrimaryLanguage { get; set; }
        public string? IsInterpreterReqd { get; set; }
        public string? GP { get; set; }
        public string? GP_Code { get; set; }
        public string? GP_Facility { get; set; }
        public string? GP_Facility_Code { get; set; }
        public string? PtAKA { get; set; }
        public string? PtLetterAddressee { get; set; }
        public string? SALUTATION { get; set; }
        public string? Ethnic { get; set; }
        public string? EthnicCode { get; set; }
        public string? DCTM_Folder_ID { get; set; }
        public int Patient_Dctm_Sts { get; set; }
    }

    [Table("PEDIGREE", Schema = "dbo")]
    public class Pedigree
    {
        [Key]
        public string PEDNO { get; set; }
        public string? PEDIGREE_NAME { get; set; }
        public int File_Dctm_Sts {  get; set; }
        public string? DCTM_Folder_ID { get; set; }
    }

    [Table("ViewPatientRelativeDetails", Schema = "dbo")] //Patients' relatives
    public class Relative
    {
        [Key]
        public int relsid { get; set; }
        public int WMFACSID { get; set; }
        public string? Name { get; set; }
        public string? RelTitle { get; set; }
        public string? RelSurname { get; set; }
        public string? RelSurnameBirth { get; set; }
        public string? RelForename1 { get; set; }
        public string? RelForename2 { get; set; }
        public string? RelAKA { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [Display(Name = "Date of Death")]
        [DataType(DataType.Date)]
        public DateTime? DOD { get; set; }
        public string? RelAffected { get; set; }
        public string? RelAlive { get; set; }
        public string? RelSex { get; set; }
        public string? Sex { get; set; }
        public string? Diagnosis { get; set; }
        public string? Status { get; set; }
        public string? SiteCode { get; set; }
        public string? Relation { get; set; }
        public int? RelCode { get; set; }
        public string? RelAdd1 { get; set; }
        public string? RelAdd2 { get; set; }
        public string? RelAdd3 { get; set; }
        public string? RelAdd4 { get; set; }
        public string? RelAdd5 { get; set; }
        public string? RelPC1 { get; set; }
    }

    [Table("RelativesDiary", Schema = "dbo")]
    public class RelativeDiary
    {
        [Key]
        public int DiaryID { get; set; }
        public int RelsID { get; set; }
        public string? DiaryAction { get; set; }
        public DateTime DiaryDate { get; set; }
        public DateTime? DiaryRec { get; set; }
        public string? DocCode { get; set; }
        public bool NotReturned { get; set; }

    }

    [Table("RelativesDiagnosis")]
    public class RelativesDiagnosis
    {
        [Key]
        public int TumourID { get; set; }
        public int RelsID { get; set; }
        //public int WMFACSID { get; set; }
        public string? Diagnosis {  get; set; }
        public string? AgeDiag {  get; set; }
        public string? Hospital { get; set; }
        public string? CRegCode { get; set; }
        [Column("Consent?")] //because some silly person named the column in the SQL table with a question mark!!
        public string? Consent { get; set; }
        public string? Confirmed { get; set; }
        public string? ConfDiagDate { get; set; }
        public Double? ConfDiagAge { get; set; }
        public string? SiteCode { get; set; }
        public string? LatCode { get; set; }
        public string? MorphCode { get; set; }
        public string? Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateReq { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateRec { get; set; }
        public string? Cons {  get; set; }
        public string? ReqBy { get; set; }
        public string? HistologyNumber { get; set; }
        public string? Grade {  get; set; }
        public string? Dukes { get; set; }
        public string? Notes {  get; set; }
    }

    [Table("ViewPatientAppointmentDetails", Schema="dbo")] //Appointment data
    public class Appointment
    {
        [Key]
        public int RefID { get; set; }
        public int MPI { get; set; }        
        public string? FamilyName { get; set; }
        public string FamilyNumber { get; set; }
        [Display(Name = "Booked Date")]
        [DataType(DataType.Date)]
        public DateTime? BOOKED_DATE { get; set; }
        [Display(Name = "Booked Time")]
        [DataType(DataType.Time)]
        public DateTime? BOOKED_TIME { get; set; }
        [Display(Name ="Appt With")]
        public string STAFF_CODE_1 { get; set; }
        public string? STAFF_CODE_2 { get; set; }
        public string? STAFF_CODE_3 { get; set; }
        public string AppType { get; set; }
        public string? Attendance { get; set; }
        public string Clinician { get; set; }
        public string? Clinician2 { get; set; }
        public string? Clinician3 { get; set; }
        public string Clinic { get; set; }
        public string FACILITY { get; set; }
        public string? Title { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public DateTime? dob { get; set; }
        public string? ADDRESS1 { get; set; }
        public string? address2 { get; set; }
        public string? address3 { get; set; }
        public string? address4 { get; set; }
        public string? postcode { get; set; }
        public string CGU_No { get; set; }
        [DataType(DataType.Time)]
        public DateTime? ArrivalTime { get; set; }
        public string? SeenBy { get; set; }
        public string? SeenByClinician { get; set; }
        public Int16? NoPatientsSeen { get; set; }
        public Int16? Duration { get; set; }
        public bool? isClockStop { get; set; }
        public string? LetterRequired { get; set; }
        public string LoginDetails { get; set; }
        public string? Notes { get; set; }
        public string? IndicationNotes { get; set; }
        public int ReferralRefID { get; set; }
        public string? LetterPrintedDate { get; set; }
        public string? PrimaryLanguage { get; set; }
        public bool? IsInterpreterReqd { get; set; }
        public bool ActiveAlerts { get; set; }
        public string? PatientInstructions { get; set; }
        public string? ClinicInstructions { get; set; }
        public string? REFERRAL_CLINICNO { get; set; }
        public string? ClinicLocation { get; set; }
        public string? CancellationReason { get; set; }
    }

    [Table("ViewPatientReferralDetails", Schema ="dbo")] //Referral data
    public class Referral
    {
        [Key]
        public int refid { get; set; }
        public int MPI { get; set; }
        public string? CGU_No {  get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string CLINICNO { get; set; }
        public string? INDICATION { get; set; }
        public string? LeadClinician { get; set; }
        public string? GC { get; set; }
        public string? AdminContact { get; set; }
        public string? ReferringClinician { get; set; }
        public string? ReferrerCode { get; set; }
        public string? ReferringFacility { get; set; }
        public string? ReferringFacilityCode { get; set; }
        [DataType(DataType.Date)]
        public DateTime? RefDate { get; set; }
        public string? RefType { get; set; }
        public string? RefClass { get; set; }
        public string? COMPLETE { get; set; }
        public DateTime? ClockStartDate { get; set; }
        public DateTime? ClockStopDate { get; set; }
        public string? PATHWAY { get; set; }
        public string? REASON_FOR_REFERRAL { get; set; }
        public bool logicaldelete { get; set; }
        public string? PATIENT_TYPE_CODE { get; set; }
        public string? GC_CODE { get; set; }
        public string? AdminContactCode { get; set; }
        public int? WeeksFromReferral { get; set; }
        public int? DaysFromReferral { get; set; }
        public DateTime? BreachDate { get; set; }
        public string? Status_Admin {  get; set; }
        public string? Clics {  get; set; }
        public string? PREGNANCY { get; set; }
        public string? UBRN { get; set; }
        public string? RefReasonCode { get; set; }
        public string? OthReason1 { get; set; }
        public string? OthReason2 { get; set; }
        public string? OthReason3 { get; set; }
        public string? OthReason4 { get; set; }
        public int? RefReasonAff { get; set; }
        public int? OthReason1Aff { get; set; }
        public int? OthReason2Aff { get; set; }
        public int? OthReason3Aff { get; set; }
        public int? OthReason4Aff { get; set; }
        public int? RefFHF { get; set; }
        public int? RefSympt { get; set; }
        public string? PtAreaCode { get; set; }
        public string? PtAreaName { get; set; }
        public string? Pathway_Subset { get; set; }
        public string? REASON_FOR_BREACH { get; set; }
    }

    [Table("MasterActivityTable", Schema="dbo")] //Any activity
    public class ActivityItem
    {
        [Key]
        public int RefID { get; set; }
        public int MPI { get; set; }
        public int? WMFACSID { get; set; }
        public string? CLINICNO { get; set; }
        public string? REFERRAL_CLINICNO { get; set; }
        [Display(Name = "Referral Date")]
        [DataType(DataType.Date)]
        public DateTime? REFERRAL_DATE { get; set; }
        [Display(Name = "Scheduled Date")]
        [DataType(DataType.Date)]
        public DateTime? DATE_SCHEDULED { get; set; }
        public DateTime? ClockStartDate { get; set; }
        public DateTime? ClockStopDate { get; set; }
        public string? COUNSELED { get; set; }        
        public DateTime? ARRIVAL_TIME { get; set; }
        public string? SEEN_BY { get; set; }
        public Int16? NOPATIENTS_SEEN { get; set; }
        public Int16? EST_DURATION_MINS { get; set; }
        public bool? ClockStop { get; set; }
        public string? LetterReq { get; set; }
        [Display(Name = "Booked Date")]
        [DataType(DataType.Date)]
        public DateTime? BOOKED_DATE { get; set; }
        [Display(Name = "Booked Time")]
        [DataType(DataType.Time)]
        public DateTime? BOOKED_TIME { get; set; }        
        public string? STAFF_CODE_1 { get; set; }
        [Display(Name = "Appointment Type")]
        public string? TYPE { get; set; }
        public string? CLASS { get; set; }
        public string? PATIENT_TYPE { get; set; }
        public string? GC { get; set; }
        [Display(Name = "Clinic Venue")]
        public string? FACILITY { get; set; }
        public string? PATHWAY { get; set; }
        public string? REF_PHYS { get; set; }
        public string? REF_FAC { get; set; }
        public string? COMPLETE {  get; set; }
        public string? REASON_FOR_REFERRAL { get; set; }
    }

    [Table("ViewPatientDiaryDetails", Schema = "dbo")]
    public class Diary
    {
        [Key]
        public int DiaryID { get; set; }
        public int WMFACSID { get; set; }
        public DateTime? DiaryDate { get; set; }
        public string? DiaryWith { get; set; }
        public string? DiaryWithName { get; set; }
        public string? DiaryCons { get; set; }
        public string? DiaryConsName { get; set; }
        public string? DiaryAction { get; set; }
        public string? DiaryText { get; set; }
        public string? DocCode { get; set; }
        public int? RefID { get; set; }
        public string? LetterFrom { get; set; }
        public string? LetterTo { get; set; }
    }

    [Table("ViewTriageDetails", Schema = "dbo")] //Cases to be triaged
    public class Triage
    {
        [Key]
        public int ICPID { get; set; }        
        public int RefID { get; set; }
        public int MPI { get; set; }
        public string ReferralPathway { get; set; }
        public string RefType { get; set; }
        public string CGU_No { get; set; }
        public string ConsultantCode { get; set; }
        public string GCCode { get; set; }
        public string Name { get; set; }
        public bool? GCToTriage { get; set; }
        public bool? ConsToTriage { get; set; }
        public bool? GCTriaged { get; set; }
        public bool? ConsTriaged { get; set; }        
        [DataType(DataType.Date)]
        public DateTime? RefDate { get; set; }
        public string LoginDetails { get; set; }
        public string Clinician { get; set; }
        public int? TreatPath { get; set; }
        public int? TreatPath2 { get; set; }
        public bool? ConsWLForSPR { get; set; }
        public string ConsultantName { get; set; }
        public string GCName { get; set; }
    }

    [Table("ViewPatientReviews", Schema = "dbo")] //Requested reviews
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public int MPI {  get; set; }
        public string CGU_No { get; set; }
        public string? Title { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? Category { get; set; }
        public string? Comments { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Created_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Planned_Date { get; set; }
        public string? Owner { get; set; }
        public string? Recipient { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Completed_Date { get; set; }
        public string? Review_Status { get; set; }
        public string? Review_Recipient { get; set; }
        public string? RecipientLogin { get; set; }
        public int? Parent_RefID { get; set; }
        public int? Child_RefID { get; set; }
    }

    [Table("ICP", Schema = "dbo")]
    public class ICP
    {
        [Key]
        public int ICPID { get; set; }
        public int REFID { get; set; }
        public int MPI { get; set; }
    }

    [Table("ICP_General", Schema = "dbo")] //General ICP
    public class ICPGeneral
    {
        [Key]
        public int ICP_General_ID { get; set; }
        public int ICPID { get; set; }
        public int? TreatPath { get; set; }
        public int? TreatPath2 { get; set; }        
        public bool? ConsWLForSPR { get; set; }
    }

    [Table("ViewPatientCancerICP", Schema = "dbo")] //Cancer ICP
    public class ICPCancer
    {
        [Key]
        public int ICP_Cancer_ID { get; set; }
        public int ICPID { get; set; }
        public int MPI { get; set; }
        public string? CGU_No {  get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public DateTime? REFERRAL_DATE { get; set; }
        //public int ICPID { get; set; }
        public int? ActOnRef { get; set; }
        public int? ReviewedOption { get; set; }
        //public string ActRefInfo { get; set; }
        public string? ActOnRefBy { get; set; }
        //public int FHFNotRet { get; set; }
        public bool FHFRev { get; set; }
        public bool PedRev { get; set; }
        public bool ConfRev { get; set; }
        public bool PathRepRev { get; set; }
        public bool RiskAssessment { get; set; }
        //public int ReviewedOption { get; set; }
        public string? FinalReviewed { get; set; }
        public string? GC_CODE { get; set; }
        public string? WaitingListClinician { get; set; }
        public string? WaitingListVenue { get; set; }
        public string? WaitingListComments { get; set; }
        public string? ReferralAction { get; set; }
        public string? Comments { get; set; }
        public string? ToBeReviewedby { get; set; }

    }

    [Table("CLIN_FACILITIES", Schema = "dbo")] //Facilities where we hold clinics
    public class ClinicVenue
    {
        [Key]
        public string FACILITY { get; set; }
        public string? NAME { get; set; }
        public string? LOCATION { get; set; }
        public string? NOTES { get; set; }
        public Int16 NON_ACTIVE { get; set; }
        public bool HasQRCode { get; set; }
    }        

    [Table("STAFF", Schema = "dbo")] //Staff members
    public class StaffMember
    {
        [Key]
        public string STAFF_CODE { get; set; }
        public string? EMPLOYEE_NUMBER { get; set; }
        public string? NAME { get; set; }
        public string? StaffTitle { get; set; }
        public string? StaffForename { get; set; }
        public string? StaffSurname { get; set; }
        public string? CLINIC_SCHEDULER_GROUPS { get; set; }
        public string? BILL_ID { get; set; }
        public string? TELEPHONE { get; set; }
        public string? POSITION { get; set; }
        public bool Clinical { get; set; }
        public bool InPost { get; set; }
        public DateTime? EMPLOYMENT_START_DATE { get; set; }
        public DateTime? EMPLOYMENT_END_DATE { get; set; }
        public string? EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string? GMCNumber { get; set; }
        public Int16? SYSTEM_ADMINISTRATOR { get; set; }
    }

    [Table("ViewPatientDiagnosisDetails", Schema = "dbo")] //Patients' diagnoses
    public class Diagnosis
    {
        [Key]
        public int ID { get; set; }
        public int MPI { get; set; }
        public string CGU_No { get; set; }
        public string Title { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string? DISEASE_CODE { get; set; }
        public string? DESCRIPTION { get; set; }        
        public string? STATUS { get; set; }
        public string? MAIN_SUB { get; set; }
        public string? NAME { get; set; }
        [Display(Name = "Date Diagnosed")]
        [DataType(DataType.Date)]
        public DateTime ENTEREDDATE { get; set; }
    }

    [Table("DISEASE", Schema = "dbo")] //List of all diseases
    public class Disease
    {
        [Key]
        public string DISEASE_CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public Int16? EXCLUDE_CLINIC { get; set; }
    }

    [Table("DISEASE_STATUS", Schema = "dbo")] //List of all statuses for diagnoses
    public class DiseaseStatus
    {
        [Key]
        public string DISEASE_STATUS { get; set; }
    }

    [Table("View_ETHNICITY_as_ListEthnicOrigin", Schema = "dbo")] //List of ethnicities
    public class Ethnicity
    {
        [Key]
        public string EthnicCode { get; set; }
        public string Ethnic { get; set; }
        public string NHSEthnicCode { get; set; }
    }

    [Table("View_Alerts", Schema = "dbo")] //Alerts
    public class Alert
    {
        [Key]
        public int AlertID { get; set; }
        public int MPI { get; set; }
        public string CGU_No { get; set; }
        public bool ProtectedAddress { get; set; }
        public DateTime EffectiveFromDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
        public string AlertType { get; set; }
        public string? Comments { get; set; }
    }      

    [Table("MasterFacilityTable", Schema = "dbo")] //External clinical facilities
    public class ExternalFacility
    {
        [Key]
        public string MasterFacilityCode { get; set; }
        public string? NAME { get; set; }
        public string? ADDRESS { get; set; }
        public string? CITY { get; set; }
        public string? STATE { get; set; }
        public string? ZIP { get; set; }
        public Int16? NONACTIVE { get; set; }
        public Int16? IS_GP_SURGERY { get; set; }
    }

    [Table("MasterClinicianTable", Schema = "dbo")] //External clinicians
    public class ExternalClinician
    {
        [Key]
        public string MasterClinicianCode { get; set; }
        public string? TITLE { get; set; }
        public string? FIRST_NAME { get; set; }
        public string? NAME { get; set; }
        public string? SPECIALITY { get; set; }
        public string? POSITION { get; set; }
        public string? FACILITY { get; set; }
        public Int16? NON_ACTIVE { get; set; }
        public Int16? Is_Gp { get; set; }
    }    

    [Table("ListRelation", Schema = "dbo")]
    public class Relation
    {
        [Key]
        public int RelCode { get; set; }
        public string relation { get; set; }
        public int ReportOrder { get; set; }

    }

    [Table("ListSex", Schema = "dbo")]
    public class Gender
    {
        [Key]
        public string SexCode { get; set; }
        public string Sex { get; set; }
    }

    [Table("PATHWAY", Schema = "dbo")]
    public class Pathway
    {
        [Key]
        public string CGU_Pathway { get; set; }
    }

    [Table("CLIN_CLASS", Schema = "dbo")]
    public class Priority
    {
        [Key]
        public int PriorityLevel { get; set; }
        public string CLASS { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IsActive { get; set; }
    }  

    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        public string MessageCode { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("ViewExternalCliniciansAndFacilities", Schema = "dbo")]
    public class ExternalCliniciansAndFacilities
    {
        [Key]
        public string MasterClinicianCode { get; set; } 
        public string? TITLE { get; set; }
        public string? FIRST_NAME { get; set; }
        public string? LAST_NAME { get; set; }
        public string? POSITION { get; set; }
        public string? SPECIALITY { get; set; }
        public Int16 Is_GP { get; set; }
        public Int16 NON_ACTIVE { get; set; }
        public string MasterFacilityCode { get; set; }
        public string? FACILITY { get; set; }
        public string? ADDRESS { get; set; }
        public string? DISTRICT { get; set; }
        public string? CITY { get; set; }
        public string? ZIP { get; set; }
    }

    [Table("ListTitle", Schema = "dbo")]
    public class PatientTitle
    {
        [Key]
        public string Title { get; set; }
    }

    [Table("ListYesNo", Schema = "dbo")]
    public class YesNo
    {
        [Key]
        public int YesNoNumber { get; set; }
        public string YesNoText { get; set; }
    }

    [Table("CLIN_OUTCOMES", Schema = "dbo")]
    public class Outcome
    {
        [Key]
        public string CLINIC_OUTCOME { get; set; }
        public string DEFAULT_CLINIC_STATUS { get; set; }
    }

    [Table("ViewPatientDictatedLetterDetails", Schema = "dbo")] //Dictated le'ahs
    public class DictatedLetter
    {
        [Key]
        public int DoTID { get; set; }
        public int? MPI { get; set; }
        public string? CGU_No { get; set; }
        public string? Patient { get; set; }
        public int? RefID { get; set; }
        public string? LetterTo { get; set; }
        public string? LetterToSalutation { get; set; }
        public string? LetterRe { get; set; }
        public string? LetterFrom { get; set; }
        public string? LetterFromCode { get; set; }
        public string? LetterContent { get; set; }
        public string? LetterContentBold { get; set; }
        public string? Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateDictated { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SecTeam { get; set; }
        public string? Consultant { get; set; }
        public string? GeneticCounsellor { get; set; }
        public string? Comments { get; set; }
        public string? Enclosures { get; set; }
    }

    [Table("DictatedLettersPatients", Schema = "dbo")] //Patients added to DOT
    public class DictatedLettersPatient
    {
        [Key]
        public int DOTPID { get; set; }
        public int DOTID { get; set; }
        public int MPI { get; set; }
        public int RefID { get; set; }
    }

    [Table("DictatedLettersCopies", Schema = "dbo")] //CC copies added to DOTs
    public class DictatedLettersCopy
    {
        [Key]
        public int CCID { get; set; }
        public int DotID { get; set; }
        public string CC { get; set; }
    }

    [Table("ViewCaseloadOverview", Schema = "dbo")] //Caseload overview
    [Keyless]
    public class Caseload
    {
        public int MPI { get; set; }
        public int RecordPrimaryKey { get; set; }
        public string StaffCode { get; set; }
        public string Type { get; set; }
        public DateTime? BookedDate { get; set; }
        public DateTime? BookedTime { get; set; }
        public string? State { get; set; }
        public string CGU_No { get; set; }
        public string Name { get; set; }
        public string Clinician { get; set; }
    }

    [Table("ViewPatientRisk", Schema = "dbo")] //Cancer risk items
    public class Risk
    {
        [Key]
        public int RiskID { get; set; }
        public int RefID { get; set; }
        public int MPI { get; set; }
        public string CGU_No { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        [DataType(DataType.Date)]
        public DateTime? RiskDate { get; set; }
        public string? RiskCode { get; set; }
        public string? RiskClinCode { get; set; }
        public string? RiskComments { get; set; }
        public Int16? IncludeLetter { get; set; }
        public double? R25_29 { get; set; }
        public double? R30_40 { get; set; }
        public double? R40_50 { get; set; }
        public double? R50_60 { get; set; }
        public string? CalculationToolUsed { get; set; }
        public string? SurvSiteCode { get; set; }
        public string? SurvSite { get; set; }
        public string? SurvFreq { get; set; }
        public string? SurvType { get; set; }
        public double? LifetimeRiskPercentage { get; set; }
        public int? SurvStartAge { get; set; }
        public int? SurvStopAge { get; set; }
        public string? Clinician { get; set; }
        public int ICPID { get; set; }
        public int ICP_Cancer_ID { get; set; }
    }

    [Table("ViewPatientSurveillance", Schema = "dbo")] //Surveillance recommendations
    public class Surveillance
    {
        [Key]
        public int SurvRecID { get; set; }
        public int RiskID { get; set; }
        public string? Clinician { get; set; }
        public int MPI { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public string? SurvFreqCode { get; set; }
        public string? SurvFreq { get; set; }
        public int? SurvStartAge { get; set; }
        public int? SurvStopAge { get; set; }
        public string? SurvSiteCode { get; set; }
        public string? SurvSite { get; set; }
        public string? SurvTypeCode { get; set; }
        public string? SurvType { get; set; }
        public string? SurvRecHoCode { get; set; }
        public int? GeneChangeID { get; set; }
        public string? GeneChangeDescription { get; set; }
    }

    [Table("ListICPCancerReviewActions", Schema = "dbo")]
    public class ICPCancerReviewAction
    {
        [Key]
        public int ID { get; set; }
        public string Action { get; set; }
        public string? description { get; set; }
        public string? DocCode { get; set; }
        public bool InUse { get; set; }
        public int ListOrder { get; set; }
    }

    [Table("ViewClinicSlots", Schema = "dbo")]
    public class ClinicSlot
    {
        [Key]
        public int SlotID { get; set; }
        public string ClinicianID { get; set; }
        public string ClinicID { get; set; }
        public string SlotStatus { get; set; }
        public string? PedNum { get; set; }
        [DataType(DataType.Date)]
        public DateTime SlotDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime SlotTime { get; set; }
        public int duration { get; set; }
        public int StartHr { get; set; }
        public int StartMin { get; set; }
        public string Clinician { get; set; }
        public string Facility { get; set; }
        public string? Comment { get; set; }
    }

    //Views rather than tables are used so we can have useful data available in the front end
    [Table("ViewPatientWaitingListDetails", Schema = "dbo")]
    [Keyless]
    public class WaitingList
    {
        public int ID { get; set; }
        public int MPI { get; set; }
        public int IntID { get; set; }
        public string? ClinicianID { get; set; }
        public string? ClinicianName { get; set; }
        public string? ClinicID { get; set; }
        public string? ClinicName { get; set; }
        public string? CGU_No { get; set; }
        public string? FIRSTNAME { get; set; }
        public string? LASTNAME { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? Duration { get; set; }
        public int PriorityLevel { get; set; }
        public string Priority { get; set; }
        public string? Comment { get; set; }
        public string? Instructions { get; set; }
    }

    [Table("ListSupervisors", Schema = "dbo")]
    public class Supervisors
    {
        [Key]
        public int ID { get; set; }
        public string StaffCode { get; set; }
        public bool isGCSupervisor { get; set; }
        public bool isConsSupervisor { get; set; }
    }

    [Table("ListAreaNames", Schema = "dbo")]
    public class AreaNames
    {
        [Key]
        public int AreaID { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string? FHCStaffCode { get; set; }
        public string? ConsCode { get; set; }
        public string? GC { get; set; }
        public string? GenCons { get; set; }
        public string? GenGC { get; set; }
        public string? GenAdmin { get; set; }
        public bool InUse { get; set; }
    }

    [Table("Clinicians_Clinics", Schema = "dbo")]
    public class ClinicDetails
    {
        [Key]
        public string Facility { get; set; }
        public string? Addressee { get; set; }
        public string? Position { get; set; }
        public string? A_Address { get; set; }
        public string? A_Town { get; set; }
        public string? A_PostCode { get; set; }
        public string? A_Salutation { get; set; }
        public string? Preamble { get; set; }
        public string? Postlude { get; set; }
        public string? Copies_To { get; set; }
        public string? ClinicSite { get; set; }
        public string? TelNo { get; set; }
        public string? Initials { get; set; }
        public string? Secretary { get; set; }
    }

    [Table("HPOTerm", Schema = "dbo")] //List of all HPO codes
    public class HPOTerm
    {
        [Key]
        public int ID { get; set; }
        public string Term { get; set; }
        public string TermCode { get; set; }
    }

    [Table("ClinicalNotesHPOTerm", Schema = "dbo")] //HPO codes applied to a clinical note (just the IDs)
    public class ClinicalNoteHPOTerms
    {
        [Key]
        public int ID { get; set; }
        public int ClinicalNoteID { get; set; }
        public int HPOTermID { get; set; }
    }
    public class HPOExtractedTerms
    {
        public int HPOTermID { get; set; }
        public string TermCode { get; set; }
        public String Term { get; set; }
    }

    [Table("ViewClinicalNoteHPOTermDetails", Schema = "dbo")] //HPO codes applied to a clinical note (including MPI and HPO data)
    public class HPOTermDetails
    {
        [Key]
        public int ID { get; set; }
        public int ClinicalNoteID { get; set; }
        public int MPI { get; set; }
        public string? Term { get; set; }
        public string? TermCode { get; set; }
    }
}
