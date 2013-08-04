namespace core.net.tests
{
    using System.Reflection;

    using Machine.Fakes.Sdk;

    public class BehaviourBase<TSubject>
        where TSubject : class
    {
        public static TSubject Type
        {
            get
            {
                return Controller.Subject;
            }
        }

        public static SpecificationController<TSubject> Controller
        {
            get
            {
                var withFakes = typeof(Machine.Fakes.WithFakes<TSubject, Machine.Fakes.Adapters.Moq.MoqFakeEngine>);
                return (SpecificationController<TSubject>)withFakes.GetField("_specificationController", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);                 
            }
        }
    }
}