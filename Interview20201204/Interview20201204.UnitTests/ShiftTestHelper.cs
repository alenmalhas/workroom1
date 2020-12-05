using Interview20201204.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview20201204.UnitTests
{
    public class ShiftTestHelper
    {
        public bool IsGroupingCorrect(List<Shift> shiftList)
        {
            bool isCompliant = true;

            for (int i = 0; i < shiftList.Count; i++)
            {
                var oneShift = shiftList[i];
                //var sameCompanyLocationList = shiftList.Where(a => a.Employer == oneShift.Employer && a.Location == oneShift.Location).ToList();
                var sameCompanyLocationIndexList = shiftList
                    .Select((item, index) => new { Item = item, Index = index })
                    .Where(a => a.Item.Employer == oneShift.Employer && a.Item.Location == oneShift.Location)
                    .Select(a => a.Index)
                    .ToList();
                if (!AreAllIndexesSequential(sameCompanyLocationIndexList))
                {
                    isCompliant = false;
                    break;
                }
            }

            return isCompliant;
        }

        private bool AreAllIndexesSequential(List<int> indexList)
        {
            var midIndex = Math.Round((decimal)indexList.Count / 2, MidpointRounding.AwayFromZero);// 5/2=3 => 0 1 2 < 3
            for (int i = 0; i < midIndex; i++)
            {
                var curIndex = indexList[i];
                var expectedIndex = curIndex + indexList.Count - (i + 1);
                var actualIndex = indexList[indexList.Count - 1];

                if (expectedIndex != actualIndex)
                    return false;
            }
            return true;
        }

        public List<Shift> CreateShiftList_18_Shifts_6_Companies_7_Locations()
        {
            var shiftList = new List<Shift>
            {
                new Shift{ Employer="C1", Location="L1", start_time=1,end_time=2, index=0 },
                new Shift{ Employer="C1", Location="L1", start_time=2,end_time=3, index=1 },
                new Shift{ Employer="C1", Location="L1", start_time=3,end_time=4, index=2 },

                new Shift{ Employer="C1", Location="L2", start_time=3,end_time=4, index=3 },
                new Shift{ Employer="C1", Location="L2", start_time=4,end_time=5, index=4 },

                new Shift{ Employer="C1", Location="L3", start_time=3,end_time=4, index=5 },
                new Shift{ Employer="C1", Location="L3", start_time=3,end_time=4, index=6 },
                new Shift{ Employer="C1", Location="L3", start_time=3,end_time=4, index=7 },
                // ----------------------------------------------------------------
                new Shift{ Employer="C2", Location="L4", start_time=3,end_time=4, index=8 },

                new Shift{ Employer="C2", Location="L5", start_time=4,end_time=5, index=9 },
                new Shift{ Employer="C2", Location="L5", start_time=5,end_time=6, index=10 },
                // ----------------------------------------------------------------
                new Shift{ Employer="C3", Location="L6", start_time=4,end_time=5, index=11 },
                new Shift{ Employer="C3", Location="L6", start_time=5,end_time=6, index=12 },
                new Shift{ Employer="C3", Location="L6", start_time=5,end_time=6, index=13 },
                // ----------------------------------------------------------------
                new Shift{ Employer="C4", Location="L1", start_time=3,end_time=4, index=14 },
                // ----------------------------------------------------------------
                new Shift{ Employer="C5", Location="L6", start_time=3,end_time=4, index=15 },
                new Shift{ Employer="C5", Location="L7", start_time=3,end_time=4, index=16 },
                // ----------------------------------------------------------------
                new Shift{ Employer="C6", Location="L1", start_time=3,end_time=4, index=17 },
                // ----------------------------------------------------------------
            };
            return shiftList;
        }

        public List<Shift> CreateShiftList_8_Shifts_4_Companies_4_Locations()
        {
            var shiftList = new List<Shift>
            {
                // ----------------------------------------------------------------
                new Shift{ Employer="C2", Location="L4", start_time=3,end_time=4, index=0 },
                new Shift{ Employer="C6", Location="L1", start_time=3,end_time=4, index=1 },
                new Shift{ Employer="C3", Location="L6", start_time=4,end_time=5, index=2 },//
                new Shift{ Employer="C2", Location="L5", start_time=4,end_time=5, index=3 },
                new Shift{ Employer="C4", Location="L1", start_time=3,end_time=4, index=4 },
                new Shift{ Employer="C3", Location="L6", start_time=5,end_time=6, index=5 },//
                new Shift{ Employer="C2", Location="L5", start_time=5,end_time=6, index=6 },
                new Shift{ Employer="C3", Location="L6", start_time=5,end_time=6, index=7 },
                // ----------------------------------------------------------------
            };
            return shiftList;
        }

        public List<Shift> CreateShiftList_4_Shifts_2_Companies_2_Locations()
        {
            var shiftList = new List<Shift>
            {
                // ----------------------------------------------------------------
                new Shift{ Employer="C2", Location="L2", start_time=3,end_time=4, index=0 },
                new Shift{ Employer="C1", Location="L1", start_time=3,end_time=4, index=1 },
                new Shift{ Employer="C2", Location="L2", start_time=4,end_time=5, index=2 },
                new Shift{ Employer="C1", Location="L1", start_time=5,end_time=6, index=3 },
                // ----------------------------------------------------------------
            };
            return shiftList;
        }
    }
}
