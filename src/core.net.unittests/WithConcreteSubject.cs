// -----------------------------------------------------------------------
// <copyright file="WithConcreteSubject.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests
{
    using System.Diagnostics.CodeAnalysis;
    using global::Machine.Fakes;
    using global::Machine.Specifications;

    /// <summary>
    /// Casts the subject as the supplied <typeparamref name="TInterface"/> but 
    /// creates it using a concrete type and fills in dependencies using the auto faking container.
    /// </summary>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    /// <typeparam name="TConcreteType">The type of the concrete type.</typeparam>
    public class WithConcreteSubject<TConcreteType, TInterface> : WithSubjectBase<TConcreteType>
        where TInterface : class
        where TConcreteType : class, TInterface
    {
        public static new TInterface Subject
        {
            get
            {
                return WithSubject<TConcreteType>.Subject;
            }
        }
    }

    /// <summary>
    /// Casts the subject as the supplied <typeparamref name="TInterface" /> but
    /// creates it using a concrete type and fills in dependencies using the auto faking container.
    /// </summary>
    /// <typeparam name="TConcreteType">The type of the concrete type.</typeparam>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class WithConcreteSubjectAndResult<TConcreteType, TInterface, TResult> :WithSubjectAndResult<TConcreteType, TResult>
        where TInterface : class
        where TConcreteType : class, TInterface
    {
        public static new TInterface Subject
        {
            get
            {
                return WithSubject<TConcreteType>.Subject;
            }
        }
    }
}