using BenchmarkDotNet.Attributes;

namespace Linq.Extras.Benchmarks
{
    public class WithIndexOnArrayBenchmark
    {
        private int[] _array;

        [Params(100, 1000, 10000)]
        public int N { get; set; }

        [GlobalSetup]
        public void Prepare()
        {
            _array = new int[N];
            for (int i = 0; i < N; i++)
            {
                _array[i] = i;
            }
        }

        [Benchmark(Baseline = true)]
        public void RegularForLoop()
        {
            var sum = 0;
            for (int i = 0; i < _array.Length; i++)
                sum += i + _array[i];
        }

        [Benchmark]
        public void ForEachLoop()
        {
            var sum = 0;
            int index = 0;
            foreach (var i in _array)
            {
                sum += index + i;
                index++;
            }
        }

        [Benchmark]
        public void WithIndex()
        {
            var sum = 0;
            foreach (var (item, index) in _array.WithIndex())
                sum += item + index;
        }
    }
}
