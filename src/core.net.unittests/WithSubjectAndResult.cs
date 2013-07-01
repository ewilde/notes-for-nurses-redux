namespace core.net.tests
{
    using Machine.Fakes;

    public class WithSubjectAndResult<TSubject, TResult> : WithSubjectBase<TSubject>
        where TSubject : class
    {
        public static TResult Result { get; set; }
    }    

    public class WithResult<TResult>
    {
        public static TResult Result { get; set; }
    }
}