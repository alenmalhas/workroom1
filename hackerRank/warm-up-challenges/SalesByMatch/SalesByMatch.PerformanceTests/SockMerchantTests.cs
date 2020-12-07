using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalesByMatch.PerformanceTests
{
    public class SockMerchantTests : SockMerchant
    {
        private int[] _sockArray;
        public SockMerchantTests()
        {
            var random = new Random();
            var arrayLength = random.Next(1, 101);
            _sockArray = new int[arrayLength];

            for (int i = 0; i < arrayLength; i++)
                _sockArray[i] = random.Next();
            
        }

        [Benchmark]
        public int GetNumberOfPairs()
        {
            //var sockIdArray = new[] { 10, 20, 20, 10, 10, 30, 50, 10, 20 };
            return base.GetNumberOfPairs(_sockArray.Length, _sockArray);
        }

        [Benchmark]
        public int GetNumberOfPairs_AsParallel()
        {
            return base.GetNumberOfPairs_AsParallel(_sockArray.Length, _sockArray);
        }
    }

    /*
[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
[SimpleJob(RuntimeMoniker.NetCoreApp30)]
[SimpleJob(RuntimeMoniker.CoreRt30)]
[SimpleJob(RuntimeMoniker.Mono)]
[RPlotExporter]
public class Md5VsSha256
{
    private SHA256 sha256 = SHA256.Create();
    private MD5 md5 = MD5.Create();
    private byte[] data;

    [Params(1000, 10000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        data = new byte[N];
        new Random(42).NextBytes(data);
    }

    [Benchmark]
    public byte[] Sha256() => sha256.ComputeHash(data);

    [Benchmark]
    public byte[] Md5() => md5.ComputeHash(data);
}
    */
}
