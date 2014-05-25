using System.Collections.Generic;
using Linq.Extras.Internal;

namespace Linq.Extras
{
    public static class ComparerEx
    {
        public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
        {
            comparer.CheckArgumentNull("comparer");
            return new ReverseComparer<T>(comparer);
        }

        sealed class ReverseComparer<T> : IComparer<T>
        {
            private readonly IComparer<T> _baseComparer;

            public ReverseComparer(IComparer<T> baseComparer)
            {
                baseComparer.CheckArgumentNull("baseComparer");
                _baseComparer = baseComparer;
            }

            #region Implementation of IComparer<T>

            public int Compare(T x, T y)
            {
                return _baseComparer.Compare(y, x);
            }

            #endregion
        }
    }
}
