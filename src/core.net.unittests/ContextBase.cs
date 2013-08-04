// -----------------------------------------------------------------------
// <copyright file="ContextBase.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests
{
    using System.Reflection;

    using Machine.Fakes;
    using Machine.Fakes.Sdk;

    public class ContextBase        
    {
        public static IFakeAccessor FakeAccessor { get; set; }        
    }
}