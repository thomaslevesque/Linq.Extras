// ReSharper disable once CheckNamespace
namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event,
        Inherited = false, AllowMultiple = false)]
    class ExcludeFromCodeCoverageAttribute : Attribute
    {
    }
}
