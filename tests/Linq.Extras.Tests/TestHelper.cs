using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;

namespace Linq.Extras.Tests
{
    static class TestHelper
    {
        public static void AssertThrowsWhenArgumentNull(Expression<Action> expr)
        {
            var realCall = expr.Body as MethodCallExpression;
            if (realCall == null)
                throw new ArgumentException("Expression body is not a method call", nameof(expr));
            
            var method = realCall.Method;
            var nullableContextAttribute =
                method.CustomAttributes
                .FirstOrDefault(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute")
                ??
                method.DeclaringType?.GetTypeInfo().CustomAttributes
                .FirstOrDefault(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute");

            if (nullableContextAttribute is null)
                throw new InvalidOperationException($"The method '{method}' is not in a nullable enabled context. Can't determine non-nullable parameters.");

            var defaultNullability = (Nullability)(byte)nullableContextAttribute.ConstructorArguments[0].Value!;

            var realArgs = realCall.Arguments;
            var parameters = method.GetParameters();
            var paramIndexes = parameters
                .Select((p, i) => new { p, i })
                .ToDictionary(x => x.p.Name!, x => x.i);
            var paramTypes = parameters
                .ToDictionary(p => p.Name!, p => p.ParameterType);

            var nonNullableRefParams = parameters
                .Where(p => !p.ParameterType.GetTypeInfo().IsValueType && GetNullability(p, defaultNullability) == Nullability.NotNull);

            foreach (var param in nonNullableRefParams)
            {
                var paramName = param.Name!;
                var args = realArgs.ToArray();
                args[paramIndexes[paramName]] = Expression.Constant(null, paramTypes[paramName]);
                var call = Expression.Call(realCall.Object, method, args);
                var lambda = Expression.Lambda<Action>(call);
                var action = lambda.Compile();
                action.Should().Throw<ArgumentNullException>($"because parameter '{paramName}' is not nullable")
                    .And.ParamName.Should().Be(paramName);
            }
        }

        private static Nullability GetNullability(ParameterInfo parameter, Nullability defaultNullability)
        {
            if (parameter.ParameterType.GetTypeInfo().IsValueType)
                return Nullability.NotNull;

            var nullableAttribute = parameter.CustomAttributes
                .FirstOrDefault(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");

            if (nullableAttribute is null)
                return defaultNullability;

            // Probably shouldn't happen
            if (nullableAttribute.ConstructorArguments.Count == 0)
                return Nullability.Nullable;

            var firstArgument = nullableAttribute.ConstructorArguments.First();
            if (firstArgument.ArgumentType == typeof(byte))
            {
                var value = (byte)firstArgument.Value!;
                return (Nullability)value;
            }
            else
            {
                var arguments = (ReadOnlyCollection<CustomAttributeTypedArgument>)firstArgument.Value!;

                // Probably shouldn't happen
                if (arguments.Count == 0)
                    return defaultNullability;

                var arg = arguments[0];
                var value = (byte)arg.Value!;

                return (Nullability)value;
            }
        }

        private enum Nullability
        {
            Oblivious = 0,
            NotNull = 1,
            Nullable = 2
        }
    }
}
