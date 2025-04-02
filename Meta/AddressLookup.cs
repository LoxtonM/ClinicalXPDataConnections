using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAddressLookup
    {
        public string GetAddress(string recipientCode, int refid, int? relID = 0);
    }

    public class AddressLookup : IAddressLookup
    {
        private readonly ClinicalContext _clinContext;
        private readonly DocumentContext _documentContext;
        private readonly ReferralData _referralData;
        private readonly IPatientData _pat;
        private readonly IRelativeData _relData;
        private readonly IExternalClinicianData _clinician;
        private readonly IExternalFacilityData _facility;

        public AddressLookup(ClinicalContext clinicalContext, DocumentContext documentContext)
        {
            _clinContext = clinicalContext;
            _documentContext = documentContext;
            _referralData = new ReferralData(_clinContext);
            _pat = new PatientData(_clinContext);           
            _relData = new RelativeData(_clinContext);
            _clinician = new ExternalClinicianData(_clinContext);
            _facility = new ExternalFacilityData(_clinContext);
        }

        public string GetAddress(string recipientCode, int refid, int? relID = 0)
        {
            string address = "";
            Referral referral = _referralData.GetReferralDetails(refid);

            if (recipientCode == "PT")
            {
                Patient pat = _pat.GetPatientDetails(referral.MPI);

                address = pat.PtLetterAddressee + Environment.NewLine + pat.ADDRESS1 + Environment.NewLine;
                if (pat.ADDRESS2 != null) { address = address + pat.ADDRESS2 + Environment.NewLine; }
                if (pat.ADDRESS3 != null) { address = address + pat.ADDRESS3 + Environment.NewLine; }
                if (pat.ADDRESS4 != null) { address = address + pat.ADDRESS4 + Environment.NewLine; }
                address = address + pat.POSTCODE;
            }

            if (recipientCode == "PTREL" && relID != 0)
            {
                Relative relative = _relData.GetRelativeDetails(relID.GetValueOrDefault());
                                
                address = relative.RelAdd1 + Environment.NewLine;
                if (relative.RelAdd2 != null)
                {
                    address = address + relative.RelAdd2 + Environment.NewLine;
                }
                address = address + relative.RelAdd3 + Environment.NewLine;
                address = address + relative.RelAdd4 + Environment.NewLine;
                address = address + relative.RelPC1;
            }

            if (recipientCode == "RD")
            {
                ExternalClinician refphys = _clinician.GetClinicianDetails(referral.ReferrerCode);
                ExternalFacility reffac = _facility.GetFacilityDetails(refphys.FACILITY);

                if (refphys.TITLE != null) { address = refphys.TITLE + " "; }
                if (refphys.FIRST_NAME != null) { address = address + refphys.FIRST_NAME + " "; }
                if (refphys.NAME != null) { address = address + refphys.NAME; }
                address = address + Environment.NewLine;
                if (reffac.ADDRESS != null) { address = address + reffac.ADDRESS + Environment.NewLine; }
                if (reffac.CITY != null) { address = address + reffac.CITY + Environment.NewLine; }
                if (reffac.STATE != null) { address = address + reffac.STATE + Environment.NewLine; }
                if (reffac.ZIP != null) { address = address + reffac.ZIP + Environment.NewLine; }
            }

            if (recipientCode == "GP")
            {
                Patient pat = _pat.GetPatientDetails(referral.MPI);
                ExternalClinician gp = _clinician.GetClinicianDetails(pat.GP_Code);
                ExternalFacility fac = _facility.GetFacilityDetails(gp.FACILITY);

                if (gp.TITLE != null) { address = gp.TITLE + " "; }
                if (gp.FIRST_NAME != null) { address = address + gp.FIRST_NAME + " "; }
                if (gp.NAME != null) { address = address + gp.NAME; }
                address = address + Environment.NewLine;
                if (fac.NAME != null) { address = address + fac.NAME + Environment.NewLine; }
                if (fac.ADDRESS != null) { address = address + fac.ADDRESS + Environment.NewLine; }
                if (fac.CITY != null) { address = address + fac.CITY + Environment.NewLine; }
                if (fac.STATE != null) { address = address + fac.STATE + Environment.NewLine; }
                if (fac.ZIP != null) { address = address + fac.ZIP + Environment.NewLine; }
            }

            return address;
        }

    }
}
