using PampaDevs.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PampaDevs.Utils.Tests.Extensions
{
    public class DynamicExtensionsTests
    {
        private class TestObject
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Fact]
        public void ToDynamic_ShouldConvertObjectToDynamic()
        {
            var testObject = new TestObject
            {
                FirstName = "firstname",
                LastName = "lastname"
            };

            var dynamicObject = testObject.ToDynamic();

            Assert.Equal("firstname", dynamicObject.FirstName);
            Assert.Equal("lastname", dynamicObject.LastName);
        }
    }
}
