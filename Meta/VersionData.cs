namespace ClinicalXPDataConnections.Meta
{
    public interface IVersionData
    {
        public string GetDLLVersion();
    }
    public class VersionData : IVersionData
    {
        public string dllVersion = "32";
        public string GetDLLVersion()
        {
            return dllVersion;
        }        

    }
}
