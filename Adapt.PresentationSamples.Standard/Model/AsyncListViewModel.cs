using System.ComponentModel;

namespace TestXamarinForms.AsyncListView
{
    public class AsyncListViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ItemModel _ItemModel;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Properties
        public ItemModel ItemModel
        {
            get
            {
                return _ItemModel;
            }

            set
            {
                _ItemModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemModel)));
            }
        }
        #endregion
    }
}
