using System;
using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Forms;

namespace TestXamarinForms.AsyncListView
{
    public partial class ItemModelProvider : ObservableCollection<ItemModel>
    {
        #region Fields
        private Timer _Timer;
        #endregion

        #region Events
        public event EventHandler ItemsLoaded;
        #endregion

        #region Constructor
        public ItemModelProvider()
        {
            _Timer = new Timer(TimerCallback, null, 3000, 0);
        }
        #endregion

        #region Private Methods
        private void TimerCallback(object state)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AddItems();
            });
        }

        private void AddItems()
        {
            Add(new ItemModel { Name = 1, Description = "First" });
            Add(new ItemModel { Name = 2, Description = "Second" });
            Add(new ItemModel { Name = 3, Description = "Third" });
            ItemsLoaded?.Invoke(this, new EventArgs());
        }
        #endregion
    }
}
