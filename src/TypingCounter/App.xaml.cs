using Core.Logging;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TypingCounter.Context.EventAggregators;
using TypingCounter.Models;
using TypingCounter.Views;

namespace TypingCounter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Container.Resolve<SettingHolder>().Load();
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            DependencyInjection.Configure(containerRegistry);
        }

        /// <summary>
        /// UIスレッドで発生した未処理例外を処理します。
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error("UIスレッドにてエラーが発生しました。", e.Exception);

            e.Handled = true;
            Container.Resolve<IEventAggregator>().GetEvent<UnhandledExceptionEvent>().Publish();
        }

        /// <summary>
        /// バックグラウンドタスクで発生した未処理例外を処理します。
        /// </summary>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Error("バックグラウンドタスクにてエラーが発生しました。", e.Exception);

            e.SetObserved();
            Container.Resolve<IEventAggregator>().GetEvent<UnhandledExceptionEvent>().Publish();
        }

        /// <summary>
        /// 最終的に処理されなかった未処理例外を処理します。
        /// </summary>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error("エラーが発生しました。アプリケーションを終了します。", (Exception)e.ExceptionObject);
        }
    }
}