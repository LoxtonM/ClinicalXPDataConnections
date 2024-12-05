using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface ITitleData
    {
        public List<PatientTitle> GetTitlesList();
    }
    public class TitleData : ITitleData
    {
        private readonly ClinicalContext _clinContext;

        public TitleData(ClinicalContext context)
        {
            _clinContext = context;
        }
        public List<PatientTitle> GetTitlesList()
        {
            IQueryable<PatientTitle> titles = from e in _clinContext.PatientTitles
                                              select e;

            return titles.ToList();
        }
    }
}
