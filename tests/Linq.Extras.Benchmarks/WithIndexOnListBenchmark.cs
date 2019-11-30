using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Linq.Extras.Benchmarks
{
    public class WithIndexOnListBenchmark
    {
        private List<int> _list;

        [Params(100, 1000, 10000)]
        public int N { get; set; }

        [GlobalSetup]
        public void Prepare()
        {
            _list = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                _list.Add(i);
            }
        }

        [Benchmark(Baseline = true)]
        public void RegularForLoop()
        {
            var sum = 0;
            for (int i = 0; i < _list.Count; i++)
                sum += i + _list[i];
        }

        [Benchmark]
        public void ForEachLoop()
        {
            var sum = 0;
            int index = 0;
            foreach (var i in _list)
            {
                sum += index + i;
                index++;
            }
        }

        [Benchmark]
        public void WithIndex()
        {
            var sum = 0;
            foreach (var (item, index) in _list.WithIndex())
                sum += item + index;
        }
    }
}
