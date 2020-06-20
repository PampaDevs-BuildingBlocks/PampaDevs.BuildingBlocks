using Microsoft.Extensions.Configuration;
using PampaDevs.BuildingBlocks.Infrastructure;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace PampaDevs.BuildingBlocks.Tests
{
    public class InfrastructureTests
    {
        [Fact]
        public void GetSystemInfo_ShouldGetSystemInfo()
        {
            var systemInfo = Assembly.GetExecutingAssembly().GetSystemInfo();

            Assert.NotNull(systemInfo);
            Assert.NotEmpty(systemInfo.AppName);
            Assert.NotEmpty(systemInfo.AssemplyVersion);
            Assert.NotEmpty(systemInfo.RuntimeFramework);
            Assert.True(systemInfo.Envs.Count > 0);
            Assert.Matches(@"Linux or OSX|Windows|Others", systemInfo.OSArchitecture);
            Assert.Matches(@"Arm|Arm64|x64|x86|Others", systemInfo.ProcessArchitecture);
        }
    }
}
