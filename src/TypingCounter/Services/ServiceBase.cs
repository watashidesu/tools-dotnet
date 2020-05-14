using Core.Logging;
using System;
using System.Threading;

namespace TypingCounter.Services
{
    public class ServiceBase
    {
        private const int _maxRetryCount = 3;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ServiceBase()
        {
        }

        /// <summary>
        /// Serviceの処理をリトライ付きで実行します。
        /// </summary>
        protected TResult DoRetryExec<TResult>(Func<TResult> func)
        {
            TResult result = default;

            for (int i = 0; i < _maxRetryCount; i++)
            {
                try
                {
                    result = func();
                    break;
                }
                catch (Exception e)
                {
                    if (IsRetryCountOver(i, e))
                    {
                        throw;
                    }
                }
            }

            return result;
        }

        private bool IsRetryCountOver(int count, Exception e)
        {
            if (count == _maxRetryCount - 1)
            {
                return true;
            }

            Logger.Warn($"リトライします。: [リトライ回数] {count + 1}回目", e);

            Thread.Sleep(1000);

            return false;
        }
    }
}