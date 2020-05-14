using Core.Wpf.Manager;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Regions;
using PropertyChanged;
using System.Collections.Generic;
using TypingCounter.Models;

namespace TypingCounter.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SettingViewModel : ViewModelBase
    {
        private readonly SettingHolder _sm;

        public IEnumerable<Swatch> Swatches { get; }
        public DelegateCommand<Swatch> ApplyPrimaryCommand { get; }
        public DelegateCommand<Swatch> ApplyAccentCommand { get; }

        public SettingViewModel(
            IRegionManager rm,
            SettingHolder sm) : base(rm)
        {
            _sm = sm;

            Swatches = new SwatchesProvider().Swatches;
            ApplyPrimaryCommand = new DelegateCommand<Swatch>(ApplyPrimary);
            ApplyAccentCommand = new DelegateCommand<Swatch>(ApplyAccent);
        }

        private void ApplyPrimary(Swatch swatch)
        {
            _sm.Update(s => s.ColorPrimary = swatch);
            ThemeManager.ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));
        }

        private void ApplyAccent(Swatch swatch)
        {
            _sm.Update(s => s.ColorAccent = swatch);
            ThemeManager.ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
        }
    }
}