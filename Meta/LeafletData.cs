using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;


namespace ClinicalXPDataConnections.Meta
{
    public interface ILeafletData
    {
        public Leaflet GetLeafletDetails(int id);
        public List<Leaflet> GetCancerLeafletsList();
        public List<Leaflet> GetGeneralLeafletsList();
        public List<Leaflet> GetAllLeafletsList();
    }
    public class LeafletData : ILeafletData
    {
        private readonly DocumentContext _docContext;

        public LeafletData(DocumentContext context)
        {
            _docContext = context;
        }

        public Leaflet GetLeafletDetails(int id)
        {
            Leaflet leaflet = _docContext.Leaflets.FirstOrDefault(l => l.ID == id);

            return leaflet;
        }
        public List<Leaflet> GetCancerLeafletsList()
        {
            IQueryable<Leaflet> leaflets = from l in _docContext.Leaflets
                                  where l.CancerLeaflet == true && l.InUse == true
                                  select l;

            return leaflets.ToList();
        }
        public List<Leaflet> GetGeneralLeafletsList()
        {
            IQueryable<Leaflet> leaflets = from l in _docContext.Leaflets
                                           where l.GeneralLeaflet == true && l.InUse == true
                                           select l;

            return leaflets.ToList();
        }
        public List<Leaflet> GetAllLeafletsList()
        {
            IQueryable<Leaflet> leaflets = from l in _docContext.Leaflets
                                           where l.InUse == true
                                           select l;

            return leaflets.ToList();
        }
    }
}
