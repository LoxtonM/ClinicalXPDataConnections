using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAreaNamesDataAsync
    {
        public Task<AreaNames> GetAreaNameDetailsByID(int id);
        public Task<AreaNames> GetAreaNameDetailsByCode(string areaCode);
        public Task<AreaNames> GetAreaNameDetailsByAreaName(string areaName);
        public Task<List<AreaNames>> GetAreaNames();
    }
    public class AreaNamesDataAsync : IAreaNamesDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public AreaNamesDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<AreaNames> GetAreaNameDetailsByID(int id)
        {
            AreaNames areaNames = await _clinContext.AreaNames.FirstOrDefaultAsync(a => a.AreaID == id);

            return areaNames;
        }

        public async Task<AreaNames> GetAreaNameDetailsByCode(string areaCode)
        {
            AreaNames areaNames = await _clinContext.AreaNames.FirstOrDefaultAsync(a => a.AreaCode == areaCode);

            return areaNames;
        }

        public async Task<AreaNames> GetAreaNameDetailsByAreaName(string areaName) //because obviously CGU_DB uses the name, not the unique identifier (which isn't recorded against the patient)
        {
            AreaNames areaNames = await _clinContext.AreaNames.FirstOrDefaultAsync(a => a.AreaName == areaName);

            return areaNames;
        }

        public async Task<List<AreaNames>> GetAreaNames()
        {
            IQueryable<AreaNames> areaNames = _clinContext.AreaNames.Where(a => a.InUse == true);

            return await areaNames.ToListAsync();
        }
    }
}
