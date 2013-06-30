namespace Edward.Wilde.Note.For.Nurses.Core.UI
{
    using TinyIoC;

    public class ObjectFactory : IObjectFactory
    {
        public TView Create<TView>() where TView : class
        {
            return TinyIoC.TinyIoCContainer.Current.Resolve<TView>();
        }

        public TView Create<TView>(NamedParameterOverloads parameterOverloads) where TView : class
        {
            return TinyIoC.TinyIoCContainer.Current.Resolve<TView>(parameterOverloads);
        }
    }
}