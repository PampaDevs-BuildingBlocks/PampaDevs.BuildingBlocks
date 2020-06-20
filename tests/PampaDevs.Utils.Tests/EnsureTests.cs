using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace PampaDevs.Utils.Tests
{
    public class EnsureTests
    {
        [Fact]
        public void That_WhenValidCondition_ShouldNotThrowException()
        {            
            Ensure.That(true);            
        }

        [Fact]
        public void That_WhenInvalidCondition_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => Ensure.That(false));
        }

        [Fact]
        public void That_WhenInvalidCondition_AndCustomException_ShouldThrowCustomException()
        {
            Assert.Throws<ArgumentException>(() => Ensure.That<ArgumentException>(false));
        }

        [Fact]
        public void Not_WhenValidCondition_ShouldNotThrowException()
        {
            Ensure.Not(false);
        }

        [Fact]
        public void Not_WhenInvalidCondition_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => Ensure.Not(true));
        }

        [Fact]
        public void Not_WhenInvalidCondition_AndCustomException_ShouldThrowCustomException()
        {
            Assert.Throws<ArgumentException>(() => Ensure.Not<ArgumentException>(true));
        }

        [Fact]
        public void NotNull_WhenNotNull_ShouldNotThrowException()
        {
            Ensure.NotNull(new object());
        }

        [Fact]
        public void NotNull_WhenNull_ShouldThrowException()
        {
            Assert.Throws<NullReferenceException>(() => Ensure.NotNull(null));
        }

        [Fact]
        public void NotNullOrEmpty_WhenNotNull_ShouldNotThrowException()
        {
            Ensure.NotNullOrEmpty("string");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NotNullOrEmpty_WhenNullOrEmpty_ShouldThrowException(string text)
        {
            Assert.Throws<Exception>(() => Ensure.NotNullOrEmpty(text));
        }

        [Fact]
        public void Equal_WhenEqual_ShouldNotThrowException()
        {
            Ensure.Equal("text", "text");
        }

        [Fact]
        public void Equal_WhenNotEqual_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => Ensure.Equal("text", "text2"));
        }

        [Fact]
        public void NotEqual_WhenNotEqual_ShouldNotThrowException()
        {
            Ensure.NotEqual("text", "text2");
        }

        [Fact]
        public void NotEqual_WhenEqual_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => Ensure.NotEqual("text", "text"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Contains_WhenContainsElement_ShouldNotThrowException(int value)
        {
            var elements = new List<int> { 1, 2, 3 };
            Ensure.Contains(elements, (element) => element == value);
        }

        [Fact]
        public void Contains_WhenNotContainsElement_ShouldThrowException()
        {
            var elements = new List<int> { 1, 2, 3 };
            Assert.Throws<Exception>(() => Ensure.Contains(elements, (element) => element == 4));
        }

        [Fact]
        public void All_WhenSatisfyCondition_ShouldNotThrowException()
        {
            var elements = new List<dynamic> { 1, 2, 3 };
            Ensure.All(elements, (element) => element.GetType() == typeof(int));
        }

        [Fact]
        public void All_WhenDontSatisfyCondition_ShouldThrowException()
        {
            var elements = new List<dynamic> { 1, 2, 3, "text" };
            Assert.Throws<Exception>(() =>
            {
                Ensure.All(elements, (element) => element.GetType() == typeof(int));
            });
        }

        [Fact]
        public void ArgumentIs_WhenSatisfyCondition_ShouldNotThrowException()
        {
            Ensure.Argument.Is(true);
        }

        [Fact]
        public void ArgumentIs_WhenDoNotSatisfyCondition_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => Ensure.Argument.Is(false));
        }

        [Fact]
        public void ArgumentIsNot_WhenSatisfyCondition_ShouldNotThrowException()
        {
            Ensure.Argument.IsNot(false);
        }

        [Fact]
        public void ArgumentIsNot_WhenDoNotSatisfyCondition_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => Ensure.Argument.IsNot(true));
        }

        [Fact]
        public void ArgumentNotNull_WhenNotNull_ShouldNotThrowException()
        {
            Ensure.Argument.NotNull(new object());
        }

        [Fact]
        public void ArgumentNotNull_WhenNull_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => Ensure.Argument.NotNull(null));
        }

        [Fact]
        public void ArgumentNotNullOrEmpty_WhenNotNullOrEmpty_ShouldNotThrowException()
        {
            Ensure.Argument.NotNullOrEmpty("text");
        }

        [Fact]
        public void ArgumentNotNullOrEmpty_WhenEmpty_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => Ensure.Argument.NotNullOrEmpty(""));
        }

        [Fact]
        public void ArgumentNotNullOrEmpty_WhenNull_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => Ensure.Argument.NotNullOrEmpty(null));
        }
    }
}
