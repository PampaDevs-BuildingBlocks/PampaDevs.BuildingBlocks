using System.Collections.Generic;

namespace PampaDevs.BuildingBlocks.Infrastructure
{
    public class SystemInfo
    {
        public SystemInfo(string osArchitecture, string osDescription, string processArchitecture, string basePath, string appName, string assemplyVersion, string runtimeFramework, Dictionary<string, object> envs)
        {
            OSArchitecture = osArchitecture;
            OSDescription = osDescription;
            ProcessArchitecture = processArchitecture;
            BasePath = basePath;
            AppName = appName;
            AssemplyVersion = assemplyVersion;
            RuntimeFramework = runtimeFramework;
            Envs = envs;
        }

        public string OSArchitecture { get; private set; }
        public string OSDescription { get; private set; }
        public string ProcessArchitecture { get; private set; }
        public string BasePath { get; private set; }
        public string AppName { get; private set; }
        public string AssemplyVersion { get; private set; }
        public string RuntimeFramework { get; private set; }
        public Dictionary<string, object> Envs { get; private set; }
    }
}
