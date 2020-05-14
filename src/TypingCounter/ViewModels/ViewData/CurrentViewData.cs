using Core.Wpf;
using Prism.Mvvm;
using PropertyChanged;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TypingCounter.Entities;

namespace TypingCounter.ViewModels.ViewData
{
    [AddINotifyPropertyChangedInterface]
    public class CurrentViewData : BindableBase
    {
        public long TotalCount => CountPerKeys?.Sum(k => k.Count) ?? 0;

        private ItemsChangeObservableCollection<CountPerKeyViewData> _countPerKeys;

        public ItemsChangeObservableCollection<CountPerKeyViewData> CountPerKeys
        {
            get => _countPerKeys;
            private set
            {
                SetProperty(ref _countPerKeys, value);
                if (_countPerKeys != null)
                {
                    _countPerKeys.CollectionItemsOrPropertyChanged += (s, e) => RaisePropertyChanged(nameof(TotalCount));
                }
                RaisePropertyChanged(nameof(TotalCount));
            }
        }

        public static CurrentViewData Of(List<Current> counts)
        {
            return new CurrentViewData
            {
                CountPerKeys = new ItemsChangeObservableCollection<CountPerKeyViewData>(counts.Select(e => CountPerKeyViewData.Of(e))),
            };
        }

        public void SetCount(Keys key, long count)
        {
            CountPerKeys.SingleOrDefault(c => c.Code == key)?.SetCount(count);
        }
    }
}