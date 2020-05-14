using NLog;
using System;
using System.Diagnostics;

namespace Core.Logging
{
    public static class Logger
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static ILogger _logger => LogManager.GetLogger($"[{new StackTrace().GetFrame(2).GetMethod().DeclaringType.Name}] [{new StackTrace().GetFrame(2).GetMethod().Name}]");

        /// <summary>
        /// DebugLog を出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public static void Debug(string message) => _logger.Debug(GetSingleLineMessage(message));

        /// <summary>
        /// InfoLog を出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public static void Info(string message) => _logger.Info(GetSingleLineMessage(message));

        /// <summary>
        /// WarnLog を出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public static void Warn(string message) => _logger.Warn(GetSingleLineMessage(message));

        /// <summary>
        /// WarnLog を出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="ex">例外</param>
        public static void Warn(string message, Exception ex) => _logger.Warn(ex, GetSingleLineMessage(message));

        /// <summary>
        /// ErrorLog を出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public static void Error(string message) => _logger.Error(GetSingleLineMessage(message));

        /// <summary>
        /// ErrorLog を出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="ex">例外</param>
        public static void Error(string message, Exception ex) => _logger.Error(ex, GetSingleLineMessage(message));

        /// <summary>
        /// 単一行メッセージを取得します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns>単一行メッセージ</returns>
        private static string GetSingleLineMessage(string message)
        {
            return message.Replace(Environment.NewLine, "###");
        }
    }
}