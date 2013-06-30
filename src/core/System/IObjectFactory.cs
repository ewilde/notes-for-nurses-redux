// -----------------------------------------------------------------------
// <copyright file="ObjectFactory.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.Note.For.Nurses.Core
{
    using TinyIoC;

    public interface IObjectFactory
    {
        TView Create<TView>() where TView : class;
        TView Create<TView>(NamedParameterOverloads parameterOverloads) where TView : class;
    }
}