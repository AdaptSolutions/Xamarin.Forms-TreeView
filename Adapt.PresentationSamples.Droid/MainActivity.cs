

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using samples = Adapt.PresentationSamples;
using xf = Xamarin.Forms;

namespace XamForms.Droid
{
    [Activity(Label = "Xamarin.Forms TreeView", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : xf.Platform.Android.FormsApplicationActivity
    {
        #region Protected Overrides
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            xf.Forms.Init(this, bundle);
            LoadApplication(new samples.App());
        }
        #endregion

        #region Public Overrides
        protected override void OnStop()
        {
            base.OnStop();
            //TODO: We should dispose when the app shuts down, but for some reason this event fires even when the app is not shutting down
            //_PresentationFactory.Dispose();
        }

        #endregion
    }
}

