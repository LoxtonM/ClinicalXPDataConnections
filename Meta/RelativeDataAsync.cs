using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRelativeDataAsync
    {
        public Task<List<Relative>> GetRelativesList(int id);
        public Task<Relative> GetRelativeDetails(int relID);
        public Task<List<Relative>> GetRelativeDetailsByName(string forename, string surname);
        public Task<List<Relation>> GetRelationsList();
        public Task<List<Gender>> GetGenderList();
    }
    public class RelativeDataAsync : IRelativeDataAsync
    {
        private readonly ClinicalContext _clinContext;      

        public RelativeDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }
                

        public async Task<List<Relative>> GetRelativesList(int id) //Get list of relatives of patient by MPI
        {
            Patient patient = await _clinContext.Patients.FirstAsync(i => i.MPI == id);
            string pedno = patient.PEDNO;
            List<Relative> relative = new List<Relative>();
                        
            if (patient.PEDNO != null)
            {
                Patient proband = await _clinContext.Patients.FirstAsync(i => i.CGU_No == pedno + ".0");
                //family file's WMFACSID is different to patient's WMFACSID
                int wmfacsID = proband.WMFACSID;

                IQueryable<Relative> rels = from r in _clinContext.Relatives
                                                where r.WMFACSID == wmfacsID
                                                select r;
                relative = await rels.ToListAsync(); //because apparently I can't create an empty Iqueryable anymore for some reason, even though it worked before!!!
            }

            return relative;
        }

        public async Task<Relative> GetRelativeDetails(int relID)
        {
            Relative rel = await _clinContext.Relatives.FirstAsync(r => r.relsid == relID);

            return rel;
        }

        public async Task<List<Relative>> GetRelativeDetailsByName(string forename, string surname)
        {
            IQueryable<Relative> rels = _clinContext.Relatives.Where(r => r.RelForename1.Contains(forename) && r.RelSurname.Contains(surname));

            return await rels.ToListAsync();
        }

        public async Task<List<Relation>> GetRelationsList()
        {
            IQueryable<Relation> item = from i in _clinContext.Relations
                       select i;

            return await item.ToListAsync();
        }

        public async Task<List<Gender>> GetGenderList()
        {
            IQueryable<Gender> item = from i in _clinContext.Genders
                       select i;

            return await item.ToListAsync();
        }
    }
}
