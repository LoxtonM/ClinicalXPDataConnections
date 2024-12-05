using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using System.Data;

namespace ClinicalXPDataConnections.Meta
{
    public interface IAlertData    
    {        
        public List<Alert> GetAlertsList(int id);
        public Alert GetAlertDetails(int id);        
    }
    public class AlertData : IAlertData
    {
        private readonly ClinicalContext _clinContext;

        public AlertData(ClinicalContext context)
        {
            _clinContext = context;
        }

        public List<Alert> GetAlertsList(int id) //Get list of alerts for patient by MPI
        {
            IQueryable<Alert> alerts = from a in _clinContext.Alert
                        where a.MPI == id & a.EffectiveToDate == null
                        orderby a.AlertID
                        select a;            

            return alerts.ToList();
        }
        

        public Alert GetAlertDetails(int id)
        {
            Alert alert = _clinContext.Alert.FirstOrDefault(a => a.AlertID == id);

            return alert;
        }
    }
}
