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
        }
    }
}
