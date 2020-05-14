using Prism.Regions;
using PropertyChanged;

namespace TypingCounter.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ErrorViewModel : ViewModelBase
    {
        public ErrorViewModel(IRegionManager rm) : base(rm)
        {
        }
    }
}