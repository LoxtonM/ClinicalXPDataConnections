using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IPhenotipsMirrorDataAsync
    {
        public Task<PhenotipsPatient> GetPhenotipsPatientByID(int mpi);
    }
    public class PhenotipsMirrorDataAsync : IPhenotipsMirrorDataAsync
    {
        private readonly ClinicalContext _context;

        public PhenotipsMirrorDataAsync(ClinicalContext context)
        {
            _context = context;
        }

        public async Task<PhenotipsPatient> GetPhenotipsPatientByID(int mpi)
        {
            PhenotipsPatient patient = await _context.PhenotipsPatient.Where(p => p.MPI == mpi).FirstAsync();

            return patient;
        }

    }
}
