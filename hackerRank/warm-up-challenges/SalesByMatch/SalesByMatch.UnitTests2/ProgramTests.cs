using FluentAssertions;
using System;
using Xunit;

namespace SalesByMatch.UnitTests
{
    public class ProgramTests
    {
        [Theory]
        [InlineData(new []{1,2,1}, 1)]
        [InlineData(new[] { 1, 2, 3 }, 0)]
        public void Test_sockMerchant(int[] sockIdArray, int expectedPairCount)
        {
            //arrange

            //act
            var pairCount = Program.sockMerchant(sockIdArray.Length, sockIdArray);

            //assert
            pairCount.Should().Be(expectedPairCount);
        }

        [Fact]
        public void test2()
        {
            true.Should().Be(true);

        }
    }
}
