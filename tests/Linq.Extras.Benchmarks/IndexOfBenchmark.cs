using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.Extras.Benchmarks
{
    public class IndexOfBenchmark
    {
        private int[] _array;

        [Params(10, 500, 10000)]
        public int N { get; set; }

        private const int SearchedValue = 42;

        [GlobalSetup]
        public void Prepare()
        {
            _array = Enumerable.Repeat(0, N).ToArray();
            _array[N - 1] = SearchedValue;
        }

        [Benchmark(Baseline = true)]
        public int RegularForLoop()
        {
            int value = SearchedValue;
            for (int i = 0; i < N; i++)
            {
                if (_array[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        [Benchmark]
        public int UsingArrayIndexOf() => Array.IndexOf(_array, SearchedValue);

        [Benchmark]
        public int UsingIndexOf() => _array.IndexOf(SearchedValue);
    }
}
