using System.Data;
using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicalXPDataConnections.Meta
{
    public interface IDictatedLetterDataAsync
    {
        public Task<List<DictatedLetter>> GetDictatedLettersList(string staffcode);
        public Task<List<DictatedLetter>> GetDictatedLettersListFull();
        public Task<List<DictatedLetter>> GetDictatedLettersForPatient(int mpi);
        public Task<DictatedLetter> GetDictatedLetterDetails(int dotID);
        public Task<List<DictatedLettersPatient>> GetDictatedLettersPatientsList(int dotID);
        public Task<List<DictatedLettersCopy>> GetDictatedLettersCopiesList(int dotID);
        public Task<DictatedLettersCopy> GetDictatedLetterCopyDetails(int id);
        public Task<List<Patient>> GetDictatedLetterPatientsList(int dotID);
    }
    public class DictatedLetterDataAsync : IDictatedLetterDataAsync
    {
        private readonly ClinicalContext _clinContext;        

        public DictatedLetterDataAsync(ClinicalContext cContext)
        {
            _clinContext = cContext;            
        }
               

        public async Task<List<DictatedLetter>> GetDictatedLettersList(string staffcode)
        {
            IQueryable<DictatedLetter> letters = from l in _clinContext.DictatedLetters
                          where l.LetterFromCode == staffcode && l.MPI != null && l.RefID != null && l.Status != "Printed"
                          orderby l.DateDictated descending
                          select l;

            return await letters.ToListAsync();
        }

        public async Task<List<DictatedLetter>> GetDictatedLettersListFull()
        {
            IQueryable<DictatedLetter> letters = from l in _clinContext.DictatedLetters
                                                 where l.MPI != null && l.RefID != null && l.Status != null
                                                 orderby l.DateDictated descending
                                                 select l;
            letters = letters.Where(l => l.Status != "Printed"); //we can't do it in one line because of course there are nulls.

            return await letters.ToListAsync();
        }

        public async Task<List<DictatedLetter>> GetDictatedLettersForPatient(int mpi)
        {
            IQueryable<DictatedLetter> letters = from l in _clinContext.DictatedLetters
                                                 where l.MPI == mpi && l.Status != "Printed"
                                                 orderby l.DateDictated descending
                                                 select l;

            return await letters.ToListAsync();
        }

        public async Task<DictatedLetter> GetDictatedLetterDetails(int dotID) //Get details of DOT letter by its DotID
        {
            DictatedLetter letter = await _clinContext.DictatedLetters.FirstAsync(l => l.DoTID == dotID);

            return letter;
        }

        public async Task<List<DictatedLettersPatient>> GetDictatedLettersPatientsList(int dotID) //Get list of patients added to a DOT letter by the DotID
        {
            IQueryable<DictatedLettersPatient> patient = from p in _clinContext.DictatedLettersPatients
                          where p.DOTID == dotID
                          select p;

            /* //do we actually need to do this???
            List<DictatedLettersPatient> patients = new List<DictatedLettersPatient>();

            foreach (var p in patient)
            {
                patients.Add(new DictatedLettersPatient() { DOTID = p.DOTID, MPI = p.MPI });
            }
            */

            return await patient.ToListAsync();
        }

        public async Task<List<DictatedLettersCopy>> GetDictatedLettersCopiesList(int dotID) //Get list of all CCs added to a DOT letter by DotID
        {
            IQueryable<DictatedLettersCopy> copies = from c in _clinContext.DictatedLettersCopies
                       where c.DotID == dotID
                       select c;            

            return await copies.ToListAsync();
        }        
        
        public async Task<DictatedLettersCopy> GetDictatedLetterCopyDetails(int id)  //Get details of a CC on a letter for deletion
        {
            DictatedLettersCopy letter = await _clinContext.DictatedLettersCopies.FirstAsync(x => x.CCID == id);

            return letter;
        }

        public async Task<List<Patient>> GetDictatedLetterPatientsList(int dotID) //Get list of all patients in the family that can be added to a DOT, by the DotID
        {
            DictatedLetter letter = await _clinContext.DictatedLetters.FirstAsync(l => l.DoTID == dotID);
            int? mpi = letter.MPI;
            Patient pat = await _clinContext.Patients.FirstAsync(p => p.MPI == mpi.GetValueOrDefault());

            IQueryable<Patient> patients = from p in _clinContext.Patients
                           where p.PEDNO == pat.PEDNO
                           select p;

            return await patients.ToListAsync();
        }
    }
}
