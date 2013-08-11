// -----------------------------------------------------------------------
// <copyright file="ContextBase.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.integrationtests.Contexts
{
    using System;

    using Machine.Fakes;

    public class ContextBase
    {
        private static bool initialized;

        static ContextBase()
        {
            Initialize();
        }

        public static TType Resolve<TType>() where TType : class
        {
            if (!initialized)
            {
                Initialize();
            }

            return TinyIoC.TinyIoCContainer.Current.Resolve<TType>();
        }

        public static IFakeAccessor FakeAccessor { get; set; }     
   
        public static TSubject Subject<TSubject>()
        {
            if (FakeAccessor == null)
            {
                throw new Exception("Please assign FakeAccessor before calling Subject()");
            }

            return (TSubject)FakeAccessor.GetType().GetProperty("Subject").GetGetMethod().Invoke(FakeAccessor, null);
        }

        private static void Initialize()
        {
            TinyIoC.TinyIoCContainer.Current.AutoRegister();
            initialized = true;
        } 
    }
}