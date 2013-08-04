namespace core.net.tests.UI
{
    using Edward.Wilde.Note.For.Nurses.Core;
    using Edward.Wilde.Note.For.Nurses.Core.Data;

    using Machine.Fakes;

    public class PatientFileUpdateManagerBehaviour
    {
        OnEstablish context = engine =>
            {
                engine.The<IObjectFactory>()
                      .WhenToldTo(call => call.Create<IPatientFileUpdateManager>())
                      .Return(engine.The<IPatientFileUpdateManager>);
            };
    }
}