using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface ILeafletDataAsync
    {
        public Task<Leaflet> GetLeafletDetails(int id);
        public Task<List<Leaflet>> GetCancerLeafletsList();
        public Task<List<Leaflet>> GetGeneralLeafletsList();
        public Task<List<Leaflet>> GetAllLeafletsList();
    }
    public class LeafletDataAsync : ILeafletDataAsync
    {
        private readonly DocumentContext _docContext;

        public LeafletDataAsync(DocumentContext context)
        {
            _docContext = context;
        }

        public async Task<Leaflet> GetLeafletDetails(int id)
        {
            Leaflet leaflet = await _docContext.Leaflets.FirstAsync(l => l.ID == id);

            return leaflet;
        }
        public async Task<List<Leaflet>> GetCancerLeafletsList()
        {
            IQueryable<Leaflet> leaflets = from l in _docContext.Leaflets
                                  where l.CancerLeaflet == true && l.InUse == true
                                  select l;

            return await leaflets.ToListAsync();
        }
        public async Task<List<Leaflet>> GetGeneralLeafletsList()
        {
            IQueryable<Leaflet> leaflets = from l in _docContext.Leaflets
                                           where l.GeneralLeaflet == true && l.InUse == true
                                           select l;

            return await leaflets.ToListAsync();
        }
        public async Task<List<Leaflet>> GetAllLeafletsList()
        {
            IQueryable<Leaflet> leaflets = from l in _docContext.Leaflets
                                           where l.InUse == true
                                           select l;

            return await leaflets.ToListAsync();
        }
    }
}
