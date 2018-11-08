using samples = Adapt.PresentationSamples;

namespace XamForms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            //var app = new samples.App(new PresentationFactory(), new Permissions(), new Geolocator(), new Clipboard(), new InAppNotification(), new FileSource());
            var app = new samples.App();

            LoadApplication(app);
        }
    }
}
