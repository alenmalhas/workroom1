using FluentAssertions;
using System;
using Xunit;

namespace SalesByMatch.UnitTests
{
    public class ProgramTests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 1 }, 1)]
        [InlineData(new[] { 1, 2, 3 }, 0)]
        [InlineData(new[] { 10, 20, 20, 10, 10, 30, 50, 10, 20 }, 3)]
        public void Test_sockMerchant(int[] sockIdArray, int expectedPairCount)
        {
            //arrange
            var sut = new SockMerchant();

            //act
            var pairCount = sut.GetNumberOfPairs(sockIdArray.Length, sockIdArray);

            //assert
            pairCount.Should().Be(expectedPairCount);
        }

    }
}
