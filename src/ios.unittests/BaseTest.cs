// -----------------------------------------------------------------------
// <copyright file="BaseTest.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace ios.unittests
{
    public class BaseTest
    {
        public static TType Resolve<TType>() where TType : class
        {
            return TinyIoC.TinyIoCContainer.Current.Resolve<TType>();
        } 
    }
}