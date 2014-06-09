using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests
{
    static class TestHelper
    {
        public static void AssertThrowsWhenArgumentNull(Expression<TestDelegate> expr, params string[] paramNames)
        {
            var realCall = expr.Body as MethodCallExpression;
            if (realCall == null)
                throw new ArgumentException("Expression body is not a method call", "expr");

            var realArgs = realCall.Arguments;
            var paramIndexes = realCall.Method.GetParameters()
                .Select((p, i) => new { p, i })
                .ToDictionary(x => x.p.Name, x => x.i);
            var paramTypes = realCall.Method.GetParameters()
                .ToDictionary(p => p.Name, p => p.ParameterType);
            
            

            foreach (var paramName in paramNames)
            {
                var args = realArgs.ToArray();
                args[paramIndexes[paramName]] = Expression.Constant(null, paramTypes[paramName]);
                var call = Expression.Call(realCall.Method, args);
                var lambda = Expression.Lambda<TestDelegate>(call);
                var action = lambda.Compile();
                var ex = Assert.Throws<ArgumentNullException>(action);
                ex.ParamName.Should().Be(paramName);
            }
        }

    }
}
