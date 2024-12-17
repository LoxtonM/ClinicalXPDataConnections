using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAreaNamesData
    {
        public AreaNames GetAreaNameDetailsByID(int id);
        public AreaNames GetAreaNameDetailsByCode(string areaCode);
        public List<AreaNames> GetAreaNames();
    }
    public class AreaNamesData : IAreaNamesData
    {
        private readonly ClinicalContext _clinContext;

        public AreaNamesData(ClinicalContext context)
        {
            _clinContext = context;
        }

        public AreaNames GetAreaNameDetailsByID(int id)
        {
            AreaNames areaNames = _clinContext.AreaNames.First(a => a.AreaID == id);

            return areaNames;
        }

        public AreaNames GetAreaNameDetailsByCode(string areaCode)
        {
            AreaNames areaNames = _clinContext.AreaNames.First(a => a.AreaCode == areaCode);

            return areaNames;
        }

        public List<AreaNames> GetAreaNames()
        {
            IQueryable<AreaNames> areaNames = _clinContext.AreaNames.Where(a => a.InUse == true);

            return areaNames.ToList();
        }
    }
}
