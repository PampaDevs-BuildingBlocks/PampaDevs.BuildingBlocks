using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PampaDevs.BuildingBlocks.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static SystemInfo GetSystemInfo(this IConfiguration config)
        {
            var basePath = Directory.GetCurrentDirectory();

            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var runtimeFramework = RuntimeInformation.FrameworkDescription;

            var envs = new Dictionary<string, object>();

            foreach (var env in config.GetChildren())
                envs.Add(env.Key, env.Value);

            var osArchitecture = GetOperationalSystemArchitecture();
            var osDescription = RuntimeInformation.OSDescription;
            var processArchitecture = GetProcessArchitecture();

            var model = new SystemInfo(
                osArchitecture,
                osDescription,
                processArchitecture,
                basePath,
                appName,
                assemblyVersion,
                runtimeFramework,
                envs);

            return model;
        }

        private static string GetOperationalSystemArchitecture()
        {
            return !RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                            ? ((RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                                ? "Linux or OSX"
                                : "Others")
                            : "Windows";
        }

        private static string GetProcessArchitecture()
        {
            return RuntimeInformation.ProcessArchitecture == Architecture.Arm
                            ? "Arm"
                            : RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                                ? "Arm64"
                                : RuntimeInformation.ProcessArchitecture == Architecture.X64
                                    ? "x64"
                                    : RuntimeInformation.ProcessArchitecture == Architecture.X86
                                        ? "x86"
                                        : "Others";
        }
    }
}
