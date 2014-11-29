using System;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Linq.Extras
{
    /// <summary>
    /// Provides helper methods to create functions and expression trees from lambda expressions, by taking advantage of generic type inference.
    /// </summary>
    [ExcludeFromCodeCoverage] // There is nothing to test here; the methods in this class just return their parameter, to take advantage of generic type inference.
    [PublicAPI]
    public static class Infer
    {
        /// <summary>
        /// Returns a <c>Func&lt;TResult&gt;</c>.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">A lambda expression representing the function to return.</param>
        /// <returns>The function passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Func<TResult> Func<TResult>(Func<TResult> func)
        {
            return func;
        }

        /// <summary>
        /// Returns an <c>Expression&lt;Func&lt;TResult&gt;&gt;</c>.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expr">A lambda expression representing the function whose expression tree will be returned.</param>
        /// <returns>The expression passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Expression<Func<TResult>> Expr<TResult>(Expression<Func<TResult>> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns a <c>Func&lt;T1, TResult&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">A lambda expression representing the function to return.</param>
        /// <returns>The function passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Func<T1, TResult> Func<T1, TResult>(Func<T1, TResult> func)
        {
            return func;
        }

        /// <summary>
        /// Returns an <c>Expression&lt;Func&lt;T1, TResult&gt;&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expr">A lambda expression representing the function whose expression tree will be returned.</param>
        /// <returns>The expression passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Expression<Func<T1, TResult>> Expr<T1, TResult>(Expression<Func<T1, TResult>> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns a <c>Func&lt;T1, T2, TResult&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">A lambda expression representing the function to return.</param>
        /// <returns>The function passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> func)
        {
            return func;
        }

        /// <summary>
        /// Returns an <c>Expression&lt;Func&lt;T1, T2, TResult&gt;&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expr">A lambda expression representing the function whose expression tree will be returned.</param>
        /// <returns>The expression passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Expression<Func<T1, T2, TResult>> Expr<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns a <c>Func&lt;T1, T2, T3, TResult&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">A lambda expression representing the function to return.</param>
        /// <returns>The function passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Func<T1, T2, T3, TResult> Func<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func)
        {
            return func;
        }

        /// <summary>
        /// Returns an <c>Expression&lt;Func&lt;T1, T2, T3, TResult&gt;&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expr">A lambda expression representing the function whose expression tree will be returned.</param>
        /// <returns>The expression passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Expression<Func<T1, T2, T3, TResult>> Expr<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns a <c>Func&lt;T1, T2, T3, T4, TResult&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="func">A lambda expression representing the function to return.</param>
        /// <returns>The function passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Func<T1, T2, T3, T4, TResult> Func<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func)
        {
            return func;
        }

        /// <summary>
        /// Returns an <c>Expression&lt;Func&lt;T1, T2, T3, T4, TResult&gt;&gt;</c>.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <typeparam name="T3">The type of the third parameter.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="expr">A lambda expression representing the function whose expression tree will be returned.</param>
        /// <returns>The expression passed as parameter.</returns>
        /// <remarks>This method just returns its parameter. It's only a helper to take advantage of generic type inference.</remarks>
        [Pure]
        public static Expression<Func<T1, T2, T3, T4, TResult>> Expr<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expr)
        {
            return expr;
        }

    }
}
