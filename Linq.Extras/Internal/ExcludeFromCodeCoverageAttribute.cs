// ReSharper disable once CheckNamespace
namespace System.Diagnostics.CodeAnalysis
{
#if !FEATURE_ExcludeFromCodeCoverageAttribute
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false)]
    sealed class ExcludeFromCodeCoverageAttribute : Attribute
    {
    }
#endif
}
