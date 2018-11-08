using Adapt.PresentationSamples;

namespace XamarinFormsTreeView
{
    public partial class App
    {
        #region Public Static Properties
        //public static IPresentationFactory PresentationFactory { get; private set; }
        //public static IPermissions CurrentPermissions { get; private set; }
        //public static IGeolocator Geolocator { get; private set; }
        //public static IClipboard Clipboard { get; set; }
        //public static IInAppNotification InAppNotification { get; set; }
        //public static IFileSource FileSource { get; set; }
        #endregion

        #region Constructor
        public App()
        {
            var mainPage = new MainPage();

            InitializeComponent();
            MainPage = mainPage;
        }
        #endregion

        #region App Events
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
