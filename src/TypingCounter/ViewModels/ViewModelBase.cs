using Core.Logging;
using Core.Utilities;
using Prism;
using Prism.Mvvm;
using Prism.Regions;
using System;
using TypingCounter.Context;
using TypingCounter.Views;

namespace TypingCounter.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IActiveAware
    {
        private readonly IRegionManager _rm;

        public ViewModelBase(IRegionManager rm)
        {
            _rm = rm;
        }

        /// <summary>
        /// 引数に与えた画面に遷移します。
        /// </summary>
        /// <param name="view">画面</param>
        protected void Navigate(string view)
        {
            Navigate(RegionNames.ContentRegion, view, null);
        }

        /// <summary>
        /// 引数に与えた画面に遷移します。
        /// </summary>
        /// <param name="view">画面</param>
        /// <param name="param">パラメータ</param>
        protected void Navigate(string view, NavigationParameters param)
        {
            Navigate(RegionNames.ContentRegion, view, param);
        }

        /// <summary>
        /// 引数に与えた画面に遷移します。
        /// </summary>
        /// <param name="region">リージョン</param>
        /// <param name="view">画面</param>
        /// <param name="param">パラメータ</param>
        protected void Navigate(string region, string view, NavigationParameters param)
        {
            _rm.RequestNavigate(region, view, NavigateResult, param);
        }

        /// <summary>
        /// 遷移処理の結果に応じた処理を行います。
        /// </summary>
        /// <param name="nr"></param>
        private void NavigateResult(NavigationResult nr)
        {
            if (nr.Error != null)
            {
                Logger.Error($"画面遷移中にエラーが発生しました。: [Uri] {nr.Context.Uri}", nr.Error);
                Navigate(nameof(Error));
            }
        }

        #region Interface

        private bool _isActive;

        /// <summary>
        /// 画面のアクティブ状態がセットされます。
        /// </summary>
        public virtual bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnIsActiveChanged();
            }
        }

        private void OnIsActiveChanged() => IsActiveChanged?.Invoke(this, new EventArgs());

        /// <summary>
        /// 画面のアクティブ状態の変更時イベントハンドラ。
        /// </summary>
        public virtual event EventHandler IsActiveChanged;

        /// <summary>
        /// 引数で渡されたコンテキストのターゲットとなる画面かどうかを返します。
        /// true を返すと View が再利用されます(新しいインスタンスを作らない)。
        /// </summary>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

        /// <summary>
        /// 画面に遷移してきたときに呼び出されます。
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            Logger.Debug($"[{GetType().Name}] に遷移しました。");
            // 引数をログ出力
            foreach (var param in navigationContext.Parameters)
            {
                Logger.Debug($"[Received param] {JsonUtil.Serialize(param)}");
            }
        }

        /// <summary>
        /// 画面から離れるときに呼び出されます。
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Logger.Debug($"[{GetType().Name}] から離れます。");
            // 引数をログ出力
            foreach (var param in navigationContext.Parameters)
            {
                Logger.Debug($"[Sended param] {JsonUtil.Serialize(param)}");
            }
        }

        #endregion Interface
    }
}