using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Linq.Extras.Tests
{
    static class Extensions
    {
        public static IEnumerable<TSource> ForbidMultipleEnumeration<TSource>(this IEnumerable<TSource> source, string message = "Sequence shouldn't be enumerated more than once.")
        {
            return new SingleEnumerationEnumerable<TSource>(source, message);
        }

        public static IEnumerable<TSource> ForbidEnumeration<TSource>(this IEnumerable<TSource> source, string message = "Sequence shouldn't be enumerated.")
        {
            return new NoEnumerationEnumerable<TSource>(message);
        }

        [ExcludeFromCodeCoverage]
        private class SingleEnumerationEnumerable<TSource> : IEnumerable<TSource>
        {
            private readonly IEnumerable<TSource> _source;
            private readonly string _message;
            private bool _enumerated;

            public SingleEnumerationEnumerable(IEnumerable<TSource> source, string message)
            {
                _source = source;
                _message = message;
            }

            public IEnumerator<TSource> GetEnumerator()
            {
                if (_enumerated)
                    throw new InvalidOperationException(_message);
                _enumerated = true;
                return _source.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class NoEnumerationEnumerable<TSource> : IEnumerable<TSource>
        {
            private readonly string _message;

            public NoEnumerationEnumerable(string message)
            {
                _message = message;
            }

            public IEnumerator<TSource> GetEnumerator()
            {
                throw new InvalidOperationException(_message);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

}
