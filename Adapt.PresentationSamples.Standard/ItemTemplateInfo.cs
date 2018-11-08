
#if(WPFSILVERLIGHT)
using System.Windows;
#else
using Xamarin.Forms;
#endif

namespace Adapt.Presentation.Controls
{
    public class ItemTemplateInfo
    {
        public DataTemplate ItemTemplate { get; set; }
        public string TypeName { get; set; }
        public string SortPropertyPath { get; set; }
    }
}
