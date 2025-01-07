using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;


namespace ClinicalXPDataConnections.Meta
{
    public interface IAuditService
    {
        public void CreateUsageAuditEntry(string staffCode, string formName, string? searchTerm = "", string? ipaddress="");
    }
    public class AuditService : IAuditService
    { 
        private readonly IConfiguration _config;

        public AuditService(IConfiguration config)
        {
            _config = config;
        }       

        public void CreateUsageAuditEntry(string staffCode, string formName, string? searchTerm = "", string? ipaddress="")
        {
            SqlConnection conn = new SqlConnection(_config.GetConnectionString("ConString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.sp_CreateAudit", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@staffCode", SqlDbType.VarChar).Value = staffCode;
            cmd.Parameters.Add("@form", SqlDbType.VarChar).Value = formName;
            cmd.Parameters.Add("@searchTerm", SqlDbType.VarChar).Value = searchTerm;
            cmd.Parameters.Add("@database", SqlDbType.VarChar).Value = "ClinicalXPDataConnections";
            if (ipaddress != "")
            {
                cmd.Parameters.Add("@machine", SqlDbType.VarChar).Value = Dns.GetHostEntry(ipaddress).HostName.Substring(0, 10);
            }
            else
            {
                cmd.Parameters.Add("@machine", SqlDbType.VarChar).Value = System.Environment.MachineName;
            }
            cmd.ExecuteNonQuery();
            conn.Close();            
        }
        

    }
}
