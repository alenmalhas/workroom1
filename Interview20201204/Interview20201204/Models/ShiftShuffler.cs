using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview20201204.Models
{
    public class ShiftShuffler
    {
        public static List<Shift> VenueShuffle(List<Shift> shiftList)
        {
            shiftList.Sort(new ShiftComparer());
            return shiftList;
        }
    }
}
