using Core.Logging;
using Core.Wpf.Manager;
using HongliangSoft.Utilities;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using TypingCounter.Context;
using TypingCounter.Context.EventAggregators;
using TypingCounter.Models;
using TypingCounter.Services;
using TypingCounter.ViewModels.ViewData;
using TypingCounter.Views;
using TypingCounter.Views.Archives;

namespace TypingCounter.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly KeyboardHook _keyboardHook;
        private readonly IRegionManager _rm;
        private readonly CurrentService _currentService;
        private readonly ArchiveService _archiveService;
        private readonly SettingHolder _settingHolder;
        private readonly ArchiveHolder _archiveHolder;
        private readonly CurrentHolder _currentHolder;

        public CurrentViewData Current { get; set; }

        public string Title { get; private set; } = "Prism Application";

        public bool IsLoading { get; private set; }
        public bool IsNightMode { get; set; }

        public DelegateCommand<string> NavigateOnDrawerCommand { get; }
        public DelegateCommand ApplyNightModeCommand { get; }
        public DelegateCommand ResetCommand { get; }

        public MainWindowViewModel(
            IEventAggregator ea,
            IRegionManager rm,
            CurrentService currentService,
            ArchiveService archiveService,
            SettingHolder settingHolder,
            CurrentHolder currentHolder,
            ArchiveHolder archiveHolder) : base(rm)
        {
            _rm = rm;
            _currentService = currentService;
            _archiveService = archiveService;
            _settingHolder = settingHolder;
            _archiveHolder = archiveHolder;
            _currentHolder = currentHolder;
            _keyboardHook = new KeyboardHook();

            _rm.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Initialize));
            _rm.RegisterViewWithRegion(RegionNames.ArchiveRegion, typeof(ArchiveList));

            Current = _currentHolder.Current;
            IsNightMode = _settingHolder.Setting.IsNightMode;
            _currentHolder.PropertyChanged += (s, e) => Current = _currentHolder.Current;
            _keyboardHook.KeyboardHooked += new KeyboardHookedEventHandler(OnKeyboardHooked);

            NavigateOnDrawerCommand = new DelegateCommand<string>(NavigateOnDrawer);
            ApplyNightModeCommand = new DelegateCommand(ApplyNightMode);
            ResetCommand = new DelegateCommand(async () => await ResetAsync(), () => !IsLoading).ObservesProperty(() => IsLoading);

            // 保存テーマの適用
            ThemeManager.ModifyTheme(theme => theme.SetPrimaryColor(_settingHolder.Setting.ColorPrimary.ExemplarHue.Color));
            ThemeManager.ModifyTheme(theme => theme.SetSecondaryColor(_settingHolder.Setting.ColorAccent.ExemplarHue.Color));
            ThemeManager.ModifyTheme(theme => theme.SetBaseTheme(_settingHolder.Setting.IsNightMode ? Theme.Dark : Theme.Light));

            ea.GetEvent<UnhandledExceptionEvent>().Subscribe(UnhandledExceptionAction, ThreadOption.UIThread);
        }

        /// <summary>
        /// ドロア上のボタンから画面遷移を行います。
        /// </summary>
        private void NavigateOnDrawer(string view)
        {
            // ドロワーを閉じる
            DrawerHost.CloseDrawerCommand.Execute(null, null);
            Navigate(view);
        }

        /// <summary>
        /// タイプ数をアーカイブし初期化します。
        /// </summary>
        private async Task ResetAsync()
        {
            IsLoading = true;
            var current = await Task.Run(() => _archiveService.Archive());
            var archive = await Task.Run(() => _archiveService.FindAll());
            _currentHolder.Set(current);
            _archiveHolder.Set(archive);
            IsLoading = false;
        }

        /// <summary>
        /// ナイトモードを適用します。
        /// </summary>
        private void ApplyNightMode()
        {
            _settingHolder.Update(s => s.IsNightMode = IsNightMode);
            ThemeManager.ModifyTheme(theme => theme.SetBaseTheme(IsNightMode ? Theme.Dark : Theme.Light));
        }

        /// <summary>
        /// マシン全体のキー操作をDBに反映します。
        /// </summary>
        private void OnKeyboardHooked(object sender, KeyboardHookedEventArgs e)
        {
            // キーアップ以外は無視
            if (e.UpDown != KeyboardUpDown.Up)
                return;
            if (IsLoading)
                return;

            try
            {
                var ret = _currentService.CountUp(e.KeyCode);
                _currentHolder.SetCount(ret.Code, ret.Count);
            }
            catch (Exception ex)
            {
                Logger.Error($"カウントアップに失敗しました。KeyCode={e.KeyCode}", ex);
            }
        }

        /// <summary>
        /// エラー画面に遷移します。
        /// </summary>
        private void UnhandledExceptionAction()
        {
            Navigate(nameof(Error));
        }
    }
}