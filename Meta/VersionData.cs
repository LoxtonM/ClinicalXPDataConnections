namespace ClinicalXPDataConnections.Meta
{
    public interface IVersionData
    {
        public string GetDLLVersion();
    }
    public class VersionData : IVersionData
    {
        public string dllVersion = "19";
        public string GetDLLVersion()
        {
            return dllVersion;
        }        

    }
}
