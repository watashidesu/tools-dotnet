using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Core.Wpf
{
    /// <summary>
    /// 要素内の変更を ItemsChange として通知します。
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/51591536/raising-event-when-property-in-observablecollection-changes"/>
    public class ItemsChangeObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler CollectionItemsChanged;

        public event PropertyChangedEventHandler CollectionItemsOrPropertyChanged;

        public ItemsChangeObservableCollection() : base()
        {
        }

        public ItemsChangeObservableCollection(List<T> list) : base(list)
        {
            RegisterPropertyChanged(Items);
            PropertyChanged += new PropertyChangedEventHandler(OnCollectionItemsOrPropertyChanged);
        }

        public ItemsChangeObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            RegisterPropertyChanged(Items);
            PropertyChanged += new PropertyChangedEventHandler(OnCollectionItemsOrPropertyChanged);
        }

        /// <summary>
        /// コレクションに追加される要素について、要素の PropertyChanged イベントに
        /// Collection の CollectionChanged イベント発火を割りつけます。
        /// </summary>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RegisterPropertyChanged(e.NewItems);
            }

            base.OnCollectionChanged(e);
        }

        private void RegisterPropertyChanged(IEnumerable items)
        {
            foreach (INotifyPropertyChanged item in items)
            {
                if (item != null)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(OnCollectionItemsChanged);
                    item.PropertyChanged += new PropertyChangedEventHandler(OnCollectionItemsOrPropertyChanged);
                }
            }
        }

        private void OnCollectionItemsChanged(object sender, PropertyChangedEventArgs e)
        {
            CollectionItemsChanged?.Invoke(sender, e);
        }

        private void OnCollectionItemsOrPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CollectionItemsOrPropertyChanged?.Invoke(sender, e);
        }
    }
}