namespace ClinicalXPDataConnections.Meta
{
    public interface IVersionData
    {
        public string GetDLLVersion();
    }
    public class VersionData : IVersionData
    {
        public string dllVersion = "23";
        public string GetDLLVersion()
        {
            return dllVersion;
        }        

    }
}
