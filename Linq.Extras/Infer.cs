using System;
using System.Linq.Expressions;

namespace Linq.Extras
{
    public static class Infer
    {
        public static Func<TResult> Func<TResult>(Func<TResult> func)
        {
            return func;
        }

        public static Expression<Func<TResult>> Expr<TResult>(Expression<Func<TResult>> expr)
        {
            return expr;
        }

        public static Func<T1, TResult> Func<T1, TResult>(Func<T1, TResult> func)
        {
            return func;
        }

        public static Expression<Func<T1, TResult>> Expr<T1, TResult>(Expression<Func<T1, TResult>> expr)
        {
            return expr;
        }

        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> func)
        {
            return func;
        }

        public static Expression<Func<T1, T2, TResult>> Expr<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expr)
        {
            return expr;
        }

        public static Func<T1, T2, T3, TResult> Func<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func)
        {
            return func;
        }

        public static Expression<Func<T1, T2, T3, TResult>> Expr<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> expr)
        {
            return expr;
        }

        public static Func<T1, T2, T3, T4, TResult> Func<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func)
        {
            return func;
        }

        public static Expression<Func<T1, T2, T3, T4, TResult>> Expr<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expr)
        {
            return expr;
        }

    }
}
