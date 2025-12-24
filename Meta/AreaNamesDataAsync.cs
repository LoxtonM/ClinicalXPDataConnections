using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAreaNamesDataAsync
    {
        public Task<AreaNames> GetAreaNameDetailsByID(int id);
        public Task<AreaNames> GetAreaNameDetailsByCode(string areaCode);
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
            AreaNames areaNames = await _clinContext.AreaNames.FirstAsync(a => a.AreaID == id);

            return areaNames;
        }

        public async Task<AreaNames> GetAreaNameDetailsByCode(string areaCode)
        {
            AreaNames areaNames = await _clinContext.AreaNames.FirstAsync(a => a.AreaCode == areaCode);

            return areaNames;
        }

        public async Task<List<AreaNames>> GetAreaNames()
        {
            IQueryable<AreaNames> areaNames = _clinContext.AreaNames.Where(a => a.InUse == true);

            return await areaNames.ToListAsync();
        }
    }
}
