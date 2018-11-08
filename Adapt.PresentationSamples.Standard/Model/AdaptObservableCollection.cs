//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Text;

//namespace TestXamarinForms.Models
//{
//    public interface IRecord
//    {

//    }

//    public class AdaptObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged, IDisposable where T : IRecord
//    {
//        #region Fields
//        private bool _busy;
//        #endregion

//        #region Events
//        public event NotifyCollectionChangedEventHandler CollectionChanged;

//        protected event PropertyChangedEventHandler PropertyChanged;
//        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
//        {
//            add
//            {
//                PropertyChanged += value;
//            }
//            remove
//            {
//                PropertyChanged -= value;
//            }
//        }
//        #endregion

//        #region Constructors
//        public AdaptObservableCollection()
//        {
//        }

//        public AdaptObservableCollection(IEnumerable<T> collection)
//        {
//            if (collection == null)
//            {
//                throw new ArgumentNullException("collection");
//            }
//            CopyFrom(collection);
//        }

//        public AdaptObservableCollection(List<T> list) : base((list != null) ? new List<T>(list.Count) : list)
//        {
//            CopyFrom(list);
//        }

//        #endregion

//        #region Private Methods
//        private void CopyFrom(IEnumerable<T> collection)
//        {
//            var items = Items;
//            if (collection != null && items != null)
//            {
//                using (var enumerator = collection.GetEnumerator())
//                {
//                    while (enumerator.MoveNext())
//                    {
//                        items.Add(enumerator.Current);
//                    }
//                }
//            }
//        }
//        private void CheckReentrancy()
//        {
//            if (_busy)
//            {
//                throw new InvalidOperationException("This is busy");
//            }
//        }

//        private void OnPropertyChanged(string propertyName)
//        {
//            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
//        }

//        #endregion

//        #region Protected Methods

//        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
//        {
//            if (CollectionChanged != null)
//            {
//                _busy = true;
//                try
//                {
//                    CollectionChanged(this, e);
//                }
//                finally
//                {
//                    _busy = false;
//                }
//            }
//        }

//        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
//        {
//            if (PropertyChanged != null)
//            {
//                _busy = true;
//                try
//                {
//                    PropertyChanged(this, e);
//                }
//                finally
//                {
//                    _busy = false;
//                }
//            }
//        }

//        protected override void ClearItems()
//        {
//            CheckReentrancy();
//            base.ClearItems();
//            OnPropertyChanged("Count");
//            OnPropertyChanged("Item[]");
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
//        }
//        protected override void RemoveItem(int index)
//        {
//            CheckReentrancy();
//            var t = base[index];
//            base.RemoveItem(index);
//            OnPropertyChanged("Count");
//            OnPropertyChanged("Item[]");
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, t, index));
//        }
//        protected override void InsertItem(int index, T item)
//        {
//            CheckReentrancy();
//            base.InsertItem(index, item);
//            OnPropertyChanged("Count");
//            OnPropertyChanged("Item[]");
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
//        }
//        protected override void SetItem(int index, T item)
//        {
//            CheckReentrancy();
//            var t = base[index];
//            base.SetItem(index, item);
//            OnPropertyChanged("Item[]");
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, t, index));
//        }

//        #endregion

//        #region IDisposable
//        public void Dispose()
//        {
//#if (MEMTRACE)
//                ObjectCounters.Disposed(this.GetType().Name);
//#endif
//            CollectionChanged = null;
//            PropertyChanged = null;
//            Clear();
//        }
//        #endregion
//    }
//}
