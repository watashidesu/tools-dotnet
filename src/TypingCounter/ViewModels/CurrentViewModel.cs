using Prism.Regions;
using PropertyChanged;
using TypingCounter.Models;
using TypingCounter.ViewModels.ViewData;

namespace TypingCounter.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CurrentViewModel : ViewModelBase
    {
        public CurrentViewData Current { get; private set; }

        public CurrentViewModel(
            IRegionManager rm,
            CurrentHolder holder) : base(rm)
        {
            Current = holder.Current;
            holder.PropertyChanged += (s, e) => Current = holder.Current;
        }
    }
}