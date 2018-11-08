
using XamarinFormsTreeView;

namespace XamForms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            var app = new XamarinFormsTreeView.App();

            LoadApplication(app);
        }
    }
}
