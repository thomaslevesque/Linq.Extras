using System;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Linq.Extras
{
    // There is nothing to test here; the methods in this class just return their parameter, to take advantage
    // of generic type inference.
    [ExcludeFromCodeCoverage]
    [PublicAPI]
    public static class Infer
    {
        [Pure]
        public static Func<TResult> Func<TResult>(Func<TResult> func)
        {
            return func;
        }

        [Pure]
        public static Expression<Func<TResult>> Expr<TResult>(Expression<Func<TResult>> expr)
        {
            return expr;
        }

        [Pure]
        public static Func<T1, TResult> Func<T1, TResult>(Func<T1, TResult> func)
        {
            return func;
        }

        [Pure]
        public static Expression<Func<T1, TResult>> Expr<T1, TResult>(Expression<Func<T1, TResult>> expr)
        {
            return expr;
        }

        [Pure]
        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> func)
        {
            return func;
        }

        [Pure]
        public static Expression<Func<T1, T2, TResult>> Expr<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expr)
        {
            return expr;
        }

        [Pure]
        public static Func<T1, T2, T3, TResult> Func<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func)
        {
            return func;
        }

        [Pure]
        public static Expression<Func<T1, T2, T3, TResult>> Expr<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> expr)
        {
            return expr;
        }

        [Pure]
        public static Func<T1, T2, T3, T4, TResult> Func<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func)
        {
            return func;
        }

        [Pure]
        public static Expression<Func<T1, T2, T3, T4, TResult>> Expr<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expr)
        {
            return expr;
        }

    }
}
