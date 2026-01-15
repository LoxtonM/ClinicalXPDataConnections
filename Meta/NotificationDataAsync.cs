using ClinicalXPDataConnections.Data;
using ClinicalXPDataConnections.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicalXPDataConnections.Meta
{
    public interface INotificationDataAsync
    {
        public Task<string> GetMessage(string messageCode);
    }
    public class NotificationDataAsync : INotificationDataAsync
    {        
        private readonly ClinicalContext _context;
        
        public NotificationDataAsync(ClinicalContext context)
        {            
            _context = context;
        }        
       
        public async Task<string> GetMessage(string messageCode)
        {
            string message = ""; 

            Notification messageNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.MessageCode == messageCode);

            if(messageNotification.IsActive) { message = messageNotification.Message; }
                        
            return message;
        }
    }
}
