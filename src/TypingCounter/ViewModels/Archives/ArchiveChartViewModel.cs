using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Prism.Regions;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TypingCounter.Models;
using TypingCounter.ViewModels.ViewData;

namespace TypingCounter.ViewModels.Archives
{
    [AddINotifyPropertyChangedInterface]
    public class ArchiveChartViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }

        public Func<double, string> XFormatter => (value) => new DateTime((long)value).ToString("d");

        public ArchiveChartViewModel(
            IRegionManager rm,
            ArchiveHolder holder) : base(rm)
        {
            var mapper = Mappers.Xy<ArchiveViewData>().X(v => v.DateTime.Ticks).Y(v => (double)v.Count);
            SeriesCollection = new SeriesCollection(mapper)
            {
                new LineSeries
                {
                    Title = "Archive",
                    LineSmoothness = 0,
                }
            };

            UpdateChart(holder.Archive.Archives);
            holder.PropertyChanged += (s, e) => UpdateChart(holder.Archive.Archives);
        }

        private void UpdateChart(ObservableCollection<ArchiveViewData> archives)
        {
            // TODO: one point だと落ちる。なぜ？
            if (archives.Count == 1)
            {
                archives = new ObservableCollection<ArchiveViewData>() { };
            }
            SeriesCollection.First().Values = new ChartValues<ArchiveViewData>(archives);
        }
    }
}