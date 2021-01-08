using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Application.Benchmark
{
    [SimpleJob(RuntimeMoniker.NetCoreApp30)]
    [RPlotExporter]
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
