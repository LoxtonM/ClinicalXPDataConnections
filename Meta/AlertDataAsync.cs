using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAlertDataAsync
    {        
        public Task<List<Alert>> GetAlertsList(int id);
        public Task<Alert> GetAlertDetails(int id);
        public Task<List<Alert>> GetAlertsListAll(int id);
    }
    public class AlertDataAsync : IAlertDataAsync
    {
        private readonly ClinicalContext _clinContext;

        public AlertDataAsync(ClinicalContext context)
        {
            _clinContext = context;
        }

        public async Task<List<Alert>> GetAlertsList(int id) //Get list of alerts for patient by MPI
        {
            IQueryable<Alert> alerts = from a in _clinContext.Alert
                        where a.MPI == id & a.EffectiveToDate == null
                        orderby a.AlertID
                        select a;            

            return await alerts.ToListAsync();
        }

        public async Task<List<Alert>> GetAlertsListAll(int id) //Get list of ALL alerts for patient by MPI
        {
            IQueryable<Alert> alerts = from a in _clinContext.Alert
                                       where a.MPI == id
                                       orderby a.AlertID
                                       select a;

            return await alerts.ToListAsync();
        }


        public async Task<Alert> GetAlertDetails(int id)
        {
            Alert alert = await _clinContext.Alert.FirstAsync(a => a.AlertID == id);

            return alert;
        }
    }
}
