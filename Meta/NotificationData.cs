using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;


namespace ClinicalXPDataConnections.Meta
{
    public interface INotificationData
    {
        public string GetMessage(string messageCode);
    }
    public class NotificationData : INotificationData
    {        
        private readonly ClinicalContext _context;
        
        public NotificationData(ClinicalContext context)
        {            
            _context = context;
        }        
       

        public string GetMessage(string messageCode)
        {
            string message = ""; 

            IQueryable<Notification> messageNotifications = _context.Notifications.Where(n => n.MessageCode == messageCode && n.IsActive == true);

            if (messageNotifications.Count() > 0) 
            { 
                message = messageNotifications.First().Message;
            }

            return message;
        }
    }
}
