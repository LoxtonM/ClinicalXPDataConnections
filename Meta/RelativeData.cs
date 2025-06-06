using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IRelativeData
    {
        public List<Relative> GetRelativesList(int id);
        public Relative GetRelativeDetails(int relID);
        public List<Relative> GetRelativeDetailsByName(string forename, string surname);
        public List<Relation> GetRelationsList();
        public List<Gender> GetGenderList();
    }
    public class RelativeData : IRelativeData
    {
        private readonly ClinicalContext _clinContext;      

        public RelativeData(ClinicalContext context)
        {
            _clinContext = context;
        }
                

        public List<Relative> GetRelativesList(int id) //Get list of relatives of patient by MPI
        {
            Patient patient = _clinContext.Patients.FirstOrDefault(i => i.MPI == id);
            string pedno = patient.PEDNO;
            
            //int wmfacsID = patient.WMFACSID;
            int wmfacsID = _clinContext.Patients.FirstOrDefault(i => i.CGU_No == pedno + ".0").WMFACSID;
            //family file's WMFACSID is different to patient's WMFACSID

            IQueryable<Relative> relative = from r in _clinContext.Relatives
                           where r.WMFACSID == wmfacsID
                           select r;           

            return relative.ToList();
        }

        public Relative GetRelativeDetails(int relID)
        {
            Relative rel = _clinContext.Relatives.FirstOrDefault(r => r.relsid == relID);

            return rel;
        }

        public List<Relative> GetRelativeDetailsByName(string forename, string surname)
        {
            List<Relative> rels = _clinContext.Relatives.Where(r => r.RelForename1.Contains(forename) && r.RelSurname.Contains(surname)).ToList();

            return rels;
        }

        public List<Relation> GetRelationsList()
        {
            IQueryable<Relation> item = from i in _clinContext.Relations
                       select i;

            return item.ToList();
        }

        public List<Gender> GetGenderList()
        {
            IQueryable<Gender> item = from i in _clinContext.Genders
                       select i;

            return item.ToList();
        }
    }
}
