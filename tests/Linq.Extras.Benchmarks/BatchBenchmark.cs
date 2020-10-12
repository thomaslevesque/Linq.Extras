using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Extras.Benchmarks
{
    public class BatchBenchmark
    {
        private int[] _array;

        [Params(100, 1000, 10000)]
        public int N { get; set; }

        [Params(10, 30, 600)]
        public int BatchSize { get; set; }

        [GlobalSetup]
        public void Prepare()
        {
            _array = Enumerable.Range(0, N).ToArray();
        }

        [Benchmark(Baseline = true)]
        public void RegularForLoop()
        {
            for (int i = 0; i < _array.Length; i += BatchSize)
            {
                int size = Math.Min(BatchSize, _array.Length - i);
                var batch = new int[size];
                for (int j = 0; j < size; j++)
                {
                    batch[j] = _array[i + j];
                }
            }
        }

        [Benchmark]
        public void UsingBatch()
        {
            foreach (var batch in _array.Batch(BatchSize))
            {
            }
        }

    }
}
