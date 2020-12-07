using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesByMatch
{
    public class SockMerchant
    {
        // works faster then GetNumberOfPairs_AsParallel
        public virtual int GetNumberOfPairs(int n, int[] ar)
        {
            var queryPairCount = from oneSock in ar
                                 group oneSock by oneSock into groupSocks
                                 select new { sockId = groupSocks.Key, count = groupSocks.Count(), PairCount = (groupSocks.Count() / 2) };
            var pairCountList = queryPairCount.ToList();
            var pairCountSum = pairCountList.Sum(a => a.PairCount);
            return pairCountSum;
        }

        // works slower then GetNumberOfPairs
        public virtual int GetNumberOfPairs_AsParallel(int n, int[] ar)
        {
            var queryPairCount = from oneSock in ar
                                 group oneSock by oneSock into groupSocks
                                 select new { sockId = groupSocks.Key, count = groupSocks.Count(), PairCount = (groupSocks.Count() / 2) };
            var pairCountList = queryPairCount.AsParallel().ToList();
            var pairCountSum = pairCountList.Sum(a => a.PairCount);
            return pairCountSum;
        }
    }
}
