using Prism.Mvvm;
using PropertyChanged;
using System.Windows.Forms;
using TypingCounter.Entities;

namespace TypingCounter.ViewModels.ViewData
{
    [AddINotifyPropertyChangedInterface]
    public class CountPerKeyViewData : BindableBase
    {
        public Keys Code { get; private set; }

        public long Count { get; private set; }

        public static CountPerKeyViewData Of(Current key)
        {
            return new CountPerKeyViewData
            {
                Code = key.Code,
                Count = key.Count,
            };
        }

        public static CountPerKeyViewData Of(Archive key)
        {
            return new CountPerKeyViewData
            {
                Code = key.Code,
                Count = key.Count,
            };
        }

        public void SetCount(long count)
        {
            Count = count;
        }
    }
}