// -----------------------------------------------------------------------
// <copyright file="ContextBase.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests
{
    using System;
    using System.Reflection;

    using Machine.Fakes;
    using Machine.Fakes.Sdk;

    public class ContextBase        
    {
        public static IFakeAccessor FakeAccessor { get; set; }     
   
        public static TSubject Subject<TSubject>()
        {
            if (FakeAccessor == null)
            {
                throw new Exception("Please assign FakeAccessor before calling Subject()");
            }

            return (TSubject)FakeAccessor.GetType().GetProperty("Subject").GetGetMethod().Invoke(FakeAccessor, null);
        }
    }
}