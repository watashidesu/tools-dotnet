using Prism.Commands;
using Prism.Regions;
using PropertyChanged;
using TypingCounter.Context;
using TypingCounter.Views.Archives;

namespace TypingCounter.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ArchiveViewModel : ViewModelBase
    {
        public DelegateCommand ShowListCommand { get; }
        public DelegateCommand ShowChartCommand { get; }

        public ArchiveViewModel(IRegionManager rm) : base(rm)
        {
            ShowListCommand = new DelegateCommand(() => Navigate(RegionNames.ArchiveRegion, nameof(ArchiveList), null));
            ShowChartCommand = new DelegateCommand(() => Navigate(RegionNames.ArchiveRegion, nameof(ArchiveChart), null));
        }
    }
}