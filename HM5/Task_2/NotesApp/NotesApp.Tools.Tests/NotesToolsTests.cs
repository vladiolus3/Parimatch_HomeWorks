using System;
using Xunit;
using Moq;
using FluentAssertions;

namespace NotesApp.Tools.Tests
{
    public class NotesToolsTests
    {
        [Fact]
        public void GeneratePositiveLong_ArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => NumberGenerator.GeneratePositiveLong(20));
        }

        [Fact]
        public void GeneratePositiveLong_Should_Success()
        {
            NumberGenerator.GeneratePositiveLong(5).Should();
        }

        [Fact]
        public void GenerateNumbersString_Should_Success_And_Return_Empty()
        {
            StringGenerator.GenerateNumbersString(0, true).Should().Be(string.Empty);
        }

        [Fact]
        public void GenerateNumbersString_ArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StringGenerator.GenerateNumbersString(-1, true));
        }

        [Fact]
        public void GenerateNumbersString_Should_Success_Without_Zero()
        {
            for (int i = 0; i < 99; i++)
            {
                var temp = StringGenerator.GenerateNumbersString(5, false);

                Assert.False(temp.StartsWith("0"));
            }
        }

        [Fact]
        public void GenerateNumbersString_Should_Success_And_Right_Length()
        {
            for (int i = 0; i < 99; i++)
            {
                var length = 10;
                var temp = StringGenerator.GenerateNumbersString(length, false);

                Assert.Equal(length, temp.Length);
            }
        }

        [Fact]
        public void GenerateNumbersString_Should_Success_And_Can_Parse()
        {
            for (int i = 0; i < 99; i++)
            {
                var length = 10;
                var temp = StringGenerator.GenerateNumbersString(length, false);

                Assert.True(long.TryParse(temp, out _) && length == temp.Length);
            }
        }

        [Fact]
        public void ShortGuid_To_And_From_Full_Cycle()
        {
            var tempGuid = Guid.NewGuid();

            string tempGuidToShort = ShortGuid.ToShortId(tempGuid);
            ShortGuid.FromShortId(tempGuidToShort).Should();
        }

        [Fact]
        public void FromShortId_Should_Success_Plus_Equal()
        {
            ShortGuid.FromShortId(Guid.NewGuid().ToShortId() + "==").Should();
        }

        [Fact]
        public void FromShortId_Should_Success_Guid_To_Guid()
        {
            ShortGuid.FromShortId(Guid.NewGuid().ToString()).Should();
        }

        [Fact]
        public void FromShortId_FormatException()
        {
            Assert.Throws<FormatException>(() => ShortGuid.FromShortId("000"));
        }

        [Fact]
        public void FromShortId_Null()
        {
            ShortGuid.FromShortId(null).Should().Be(null);
        }
    }
}
