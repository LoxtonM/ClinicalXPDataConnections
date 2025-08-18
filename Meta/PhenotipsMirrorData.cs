using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface IPhenotipsMirrorData
    {
        public PhenotipsPatient GetPhenotipsPatientByID(int mpi);
    }
    public class PhenotipsMirrorData : IPhenotipsMirrorData
    {
        private readonly ClinicalContext _context;

        public PhenotipsMirrorData(ClinicalContext context)
        {
            _context = context;
        }

        public PhenotipsPatient GetPhenotipsPatientByID(int mpi)
        {
            PhenotipsPatient patient = _context.PhenotipsPatient.Where(p => p.MPI == mpi).FirstOrDefault();

            return patient;
        }

    }
}
