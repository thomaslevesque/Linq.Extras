using System;
using NAssert = NUnit.Framework.Assert;

namespace Linq.Extras.Tests
{
    static class AssertExtensions
    {
        public static IAssertion<T> Assert<T>(this T actualValue)
        {
            return new Assertion<T>(actualValue);
        }
        public interface IAssertion<T>
        {
            IAssertion<T> IsEqualTo(T expected);
            IAssertion<T> IsNotEqualTo(T expected);
            IAssertion<T> Verifies(Func<T, bool> predicate);
        }
        class Assertion<T> : IAssertion<T>
        {
            private readonly T _actualValue;

            public Assertion(T actualValue)
            {
                _actualValue = actualValue;
            }

            public IAssertion<T> IsEqualTo(T expected)
            {
                NAssert.AreEqual(_actualValue, expected);
                return this;
            }

            public IAssertion<T> IsNotEqualTo(T expected)
            {
                NAssert.AreEqual(_actualValue, expected);
                return this;
            }

            public IAssertion<T> Verifies(Func<T, bool> predicate)
            {
                NAssert.IsTrue(predicate(_actualValue));
                return this;
            }
        }
    }
}
