using System.ComponentModel;
using System.Diagnostics;

namespace TestXamarinForms.AsyncListView
{
    public class ItemModel : INotifyPropertyChanged
    {
        #region Fields
        private int _Name;
        private string _Description;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Properties
        public int Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }
        #endregion

        #region Public Methods
        public override bool Equals(object obj)
        {
            var itemModel = obj as ItemModel;
            if (itemModel == null)
            {
                return false;
            }

            var returnValue = Name.Equals(itemModel.Name);

            Debug.WriteLine($"An {nameof(ItemModel)} was tested for equality. Equal: {returnValue}");

            return returnValue;
        }

        public override int GetHashCode()
        {
            Debug.WriteLine($"{nameof(GetHashCode)} was called on an {nameof(ItemModel)}");
            return Name;
        }

        #endregion
    }
}
