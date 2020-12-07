using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SalesByMatch.UnitTests
{
    public class Class1Tests
    {
        [Fact]
        public void GetInt()
        {
            var sut = new Class1();

            var result = sut.GetInt();

            result.Should().Be(1);
        }
    }
}
