// -----------------------------------------------------------------------
// <copyright file="ContextBase.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.integrationtests.Contexts
{
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

        private static void Initialize()
        {
            TinyIoC.TinyIoCContainer.Current.AutoRegister();
            initialized = true;
        } 
    }
}