using Prism.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TypingCounter.Entities;

namespace TypingCounter.ViewModels.ViewData
{
    [AddINotifyPropertyChangedInterface]
    public class ArchivesViewData : BindableBase
    {
        public decimal TotalCount => Archives?.Sum(k => k.Count) ?? 0;

        private ObservableCollection<ArchiveViewData> _archives;

        public ObservableCollection<ArchiveViewData> Archives
        {
            get => _archives;
            private set
            {
                SetProperty(ref _archives, value);
                if (_archives != null)
                {
                    _archives.CollectionChanged += (s, e) => RaisePropertyChanged(nameof(TotalCount));
                }
                RaisePropertyChanged(nameof(TotalCount));
            }
        }

        public static ArchivesViewData Of(List<ArchiveDate> archives)
        {
            return new ArchivesViewData
            {
                Archives = new ObservableCollection<ArchiveViewData>(archives.OrderByDescending(e => e.DateTime).Select(e => ArchiveViewData.Of(e))),
            };
        }
    }

    public class ArchiveViewData
    {
        public decimal Count { get; private set; }

        public DateTime DateTime { get; private set; }

        public List<CountPerKeyViewData> CountPerKeys { get; private set; }

        public static ArchiveViewData Of(ArchiveDate archive)
        {
            var keys = new List<CountPerKeyViewData>(
                archive.Archives
                .OrderByDescending(ak => ak.Count)
                .ThenBy(ak => ak.Code)
                .Select(ak => CountPerKeyViewData.Of(ak)));

            var ret = new ArchiveViewData
            {
                Count = archive.Archives.Sum(ak => ak.Count),
                DateTime = archive.DateTime,
                CountPerKeys = keys,
            };
            return ret;
        }
    }
}