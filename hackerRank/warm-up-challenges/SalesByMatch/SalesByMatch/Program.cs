using System;
using System.IO;
using System.Linq;

namespace SalesByMatch
{
    public class Program
    {
        // Complete the sockMerchant function below.
        public static int sockMerchant(int n, int[] ar)
        {
            var queryPairCount = from oneSock in ar
                                 group oneSock by oneSock into groupSocks
                                 where groupSocks.Count() % 2 == 0
                                 select new { sockId = groupSocks.Key, count = groupSocks.Count() };
            var list1 = queryPairCount.ToList();
            return list1.Count;
        }

        public static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int n = Convert.ToInt32(Console.ReadLine());

            int[] ar = Array.ConvertAll(
                Console.ReadLine().Split(' '),
                arTemp => Convert.ToInt32(arTemp)
                );
            int result = sockMerchant(n, ar);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
