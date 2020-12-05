using FluentAssertions;
using Interview20201204.Models;
using Xunit;

namespace Interview20201204.UnitTests
{
    public class ShiftTests
    {
        private readonly ShiftTestHelper _testHelper = new ShiftTestHelper();
        private readonly ShiftComparer _shiftComparer = new ShiftComparer();
        

        [Fact]
        public void Test_With_4_Shifts_With_2_Companies()
        {
            var shiftList = _testHelper.CreateShiftList_4_Shifts_2_Companies_2_Locations();
            //shiftList.Sort();
            shiftList.Sort(_shiftComparer);
            
            bool compliant = _testHelper.IsGroupingCorrect(shiftList);
            var comparisionLog = _shiftComparer.GetComparisionLog();
            compliant.Should().BeTrue(comparisionLog);
        }

        [Fact]
        public void Test_With_8_Shifts_With_4_Companies()
        {
            var shiftList = _testHelper.CreateShiftList_8_Shifts_4_Companies_4_Locations();
            //shiftList.Sort();
            shiftList.Sort(_shiftComparer);

            var compliant = _testHelper.IsGroupingCorrect(shiftList);
            var comparisionLog = _shiftComparer.GetComparisionLog();
            compliant.Should().BeTrue(comparisionLog);
        }

    }
}
