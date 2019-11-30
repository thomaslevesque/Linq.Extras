using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Linq.Extras.Benchmarks
{
    public class WithIndexOnEnumerableBenchmark
    {
        private IEnumerable<int> _enumerable;

        [Params(100, 1000, 10000)]
        public int N { get; set; }

        [GlobalSetup]
        public void Prepare()
        {
            _enumerable = Enumerable.Range(0, N);
        }

        [Benchmark(Baseline = true)]
        public void ForEachLoop()
        {
            var sum = 0;
            int index = 0;
            foreach (var i in _enumerable)
            {
                sum += index + i;
                index++;
            }
        }

        [Benchmark]
        public void WithIndex()
        {
            var sum = 0;
            foreach (var (item, index) in _enumerable.WithIndex())
                sum += item + index;
        }
    }
}
