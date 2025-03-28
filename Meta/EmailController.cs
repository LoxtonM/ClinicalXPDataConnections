namespace ClinicalXPDataConnections.Meta
{
    public class EmailController
    {        
        public void CreateEmailDraft(string recipient, string subject, string emailContent)
        {
            emailContent = emailContent.Replace(" ", "_");

            System.Diagnostics.Process.Start("C:\\Program Files\\Microsoft Office\\root\\Office16\\OUTLOOK.EXE", "/c ipm.note /m " + $"mailto:{recipient}?subject={subject}&body={emailContent}");            
        }
        
    }
}
