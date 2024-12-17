﻿namespace ClinicalXPDataConnections.Meta
{
    public interface IVersionData
    {
        public string GetDLLVersion();
    }
    public class VersionData : IVersionData
    {
        public string dllVersion = "11";
        public string GetDLLVersion()
        {
            return dllVersion;
        }        

    }
}
