namespace ClinicalXPDataConnections.Meta
{
    public interface IVersionData
    {
        public string GetDLLVersion();
    }
    public class VersionData : IVersionData
    {
        public string dllVersion = "28";
        public string GetDLLVersion()
        {
            return dllVersion;
        }        

    }
}
