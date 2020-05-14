using Prism.Regions;
using PropertyChanged;
using TypingCounter.Models;
using TypingCounter.ViewModels.ViewData;

namespace TypingCounter.ViewModels.Archives
{
    [AddINotifyPropertyChangedInterface]
    public class ArchiveListViewModel : ViewModelBase
    {
        public ArchivesViewData Archive { get; private set; }

        public ArchiveListViewModel(
            IRegionManager rm,
            ArchiveHolder holder) : base(rm)
        {
            Archive = holder.Archive;
            holder.PropertyChanged += (s, e) => Archive = holder.Archive;
        }
    }
}