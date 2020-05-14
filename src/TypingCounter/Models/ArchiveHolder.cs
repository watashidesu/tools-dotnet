using Prism.Mvvm;
using PropertyChanged;
using System.Collections.Generic;
using TypingCounter.Entities;
using TypingCounter.ViewModels.ViewData;

namespace TypingCounter.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ArchiveHolder : BindableBase
    {
        public ArchivesViewData Archive { get; private set; }

        public ArchiveHolder()
        {
            Archive = new ArchivesViewData();
        }

        public void Set(List<ArchiveDate> archives)
        {
            Archive = ArchivesViewData.Of(archives);
        }
    }
}