namespace Edward.Wilde.Note.For.Nurses.Core.UI
{
    using TinyIoC;

    public class ObjectFactory : IObjectFactory
    {
        public TInstance Create<TInstance>() where TInstance : class
        {
            return TinyIoC.TinyIoCContainer.Current.Resolve<TInstance>();
        }

        public TInstance Create<TInstance>(NamedParameterOverloads parameterOverloads) where TInstance : class
        {
            return TinyIoC.TinyIoCContainer.Current.Resolve<TInstance>(parameterOverloads);
        }
    }
}