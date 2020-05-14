using Prism.Commands;
using Prism.Regions;
using PropertyChanged;
using System.Threading.Tasks;
using TypingCounter.Models;
using TypingCounter.Services;
using TypingCounter.Views;

namespace TypingCounter.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class InitializeViewModel : ViewModelBase
    {
        private readonly CurrentService _currentService;
        private readonly ArchiveService _archiveService;
        private readonly CurrentHolder _currentHolder;
        private readonly ArchiveHolder _archiveHolder;

        public string State { get; set; }

        public DelegateCommand LoadedCommand { get; }

        public InitializeViewModel(
            IRegionManager rm,
            CurrentService currentService,
            ArchiveService archiveService,
            CurrentHolder currentHolder,
            ArchiveHolder archiveHolder) : base(rm)
        {
            _currentService = currentService;
            _archiveService = archiveService;
            _currentHolder = currentHolder;
            _archiveHolder = archiveHolder;

            LoadedCommand = new DelegateCommand(async () => await LoadedAsync());
        }

        private async Task LoadedAsync()
        {
            State = "Now loading...";
            // 初期データ作成
            _currentHolder.Set(await Task.Run(() => _currentService.Initialize()));
            _archiveHolder.Set(await Task.Run(() => _archiveService.FindAll()));
            State = "Finish!";

            Navigate(nameof(Current));
        }
    }
}