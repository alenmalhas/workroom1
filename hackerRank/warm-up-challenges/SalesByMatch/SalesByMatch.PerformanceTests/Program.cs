using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace SalesByMatch.PerformanceTests
{
    class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                //.With(ConfigOptions.DisableOptimizationsValidator);
                .WithOptions(ConfigOptions.DisableOptimizationsValidator);
            var summary = BenchmarkRunner.Run<SockMerchantTests>(config);
            //var summary = BenchmarkRunner.Run<SockMerchantTests>();
        }
    }
}
