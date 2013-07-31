// -----------------------------------------------------------------------
// <copyright file="WithConcreteUnmockedSubject.cs">
// Copyright Edward Wilde (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace core.net.tests
{
    using System;
    using System.Linq.Expressions;

    using Machine.Fakes;
    using Machine.Fakes.Sdk;

    public class WithConcreteUnmockedSubjectAndResult<TConcreteType, TInterface, TResult> : WithConcreteUnmockedSubject<TConcreteType, TInterface>
        where TInterface : class
        where TConcreteType : class, TInterface
    {
        public static TResult Result { get; set; }
    }

    public class WithConcreteUnmockedSubject<TConcreteType, TInterface> : WithSubjectUnmockedBase<TConcreteType>
        where TInterface : class
        where TConcreteType : class, TInterface
    {
        public static new TInterface Subject
        {
            get
            {
                return WithSubjectUnmockedBase<TConcreteType>.Subject;
            }
        }
    }

    public abstract class WithSubjectUnmockedBase<TSubject> : WithSubject<TSubject, TinyIocEngine> where TSubject: class
    {
       public static TType Resolve<TType>() where TType : class
       {
           return TinyIocEngine.Resolve<TType>();
       }
    }

    public class TinyIocEngine : RewritingFakeEngine
    {
        private static bool initialized;

        static TinyIocEngine()
        {
            Initialize();
        }

        public TinyIocEngine() : base(new TinyIocExpressionWriter())
        {
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

        public override object CreateFake(Type interfaceType, params object[] args)
        {
            return TinyIoC.TinyIoCContainer.Current.Resolve(interfaceType);
        }

        public override T PartialMock<T>(params object[] args)
        {
            throw new NotImplementedException();
        }

        protected override IMethodCallOccurrence OnVerifyBehaviorWasExecuted<TFake>(TFake fake, Expression<Action<TFake>> func)
        {
            throw new NotImplementedException();
        }

        protected override IQueryOptions<TReturnValue> OnSetUpQueryBehaviorFor<TFake, TReturnValue>(TFake fake, Expression<Func<TFake, TReturnValue>> func)
        {
            throw new NotImplementedException();
        }

        protected override void OnVerifyBehaviorWasNotExecuted<TFake>(TFake fake, Expression<Action<TFake>> func)
        {
            throw new NotImplementedException();
        }

        protected override ICommandOptions OnSetUpCommandBehaviorFor<TFake>(TFake fake, Expression<Action<TFake>> func)
        {
            throw new NotImplementedException();
        }
    }

    public class TinyIocExpressionWriter : AbstractExpressionRewriter
    {
        
    }
}