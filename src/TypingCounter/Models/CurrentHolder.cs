using Prism.Mvvm;
using PropertyChanged;
using System.Collections.Generic;
using System.Windows.Forms;
using TypingCounter.Entities;
using TypingCounter.ViewModels.ViewData;

namespace TypingCounter.Models
{
    [AddINotifyPropertyChangedInterface]
    public class CurrentHolder : BindableBase
    {
        public CurrentViewData Current { get; private set; }

        public CurrentHolder()
        {
            Current = new CurrentViewData();
        }

        public void Set(List<Current> counts)
        {
            Current = CurrentViewData.Of(counts);
        }

        public void SetCount(Keys keys, long count)
        {
            Current.SetCount(keys, count);
        }
    }
}