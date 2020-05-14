using Prism.Events;

namespace TypingCounter.Context.EventAggregators
{
    /// <summary>
    /// 汎用的な例外発生時イベント。
    /// グローバル例外ハンドリングの用途で使用し、
    /// 基本的には固有の例外処理を実装してください。
    /// </summary>
    public class UnhandledExceptionEvent : PubSubEvent
    {
    }
}