using System;
using System.IO;
using System.Linq;

namespace SalesByMatch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int n = Convert.ToInt32(Console.ReadLine());

            int[] ar = Array.ConvertAll(
                Console.ReadLine().Split(' '),
                arTemp => Convert.ToInt32(arTemp)
                );
            var sockMerchant = new SockMerchant();
            int result = sockMerchant.GetNumberOfPairs(n, ar);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
