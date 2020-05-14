using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TypingCounter.Context.Orm;
using TypingCounter.Entities;

namespace TypingCounter.Services
{
    /// <summary>
    /// 現在のタイプ数に関する操作です。
    /// </summary>
    public class CurrentService : ServiceBase
    {
        /// <summary>
        /// 初期表示タイプ数を取得します。
        /// データが無い場合は登録します。
        /// </summary>
        public List<Current> Initialize()
        {
            using var context = new ApplicationDbContext();
            return context.Tx(() =>
            {
                var currents = context.Current.ToList();
                if (!currents.Any())
                {
                    currents = Current.CreateInitialKeyList();
                    context.Current.AddRange(currents);
                    context.SaveChanges();
                }
                return currents;
            });
        }

        /// <summary>
        /// 引数に与えられたコードに対応するタイプ数をインクリメントします。
        /// </summary>
        public Current CountUp(Keys code)
        {
            using var context = new ApplicationDbContext();
            return context.Tx(() =>
            {
                var target = context.Current.Single(c => c.Code == code);
                target.Count++;
                context.SaveChanges();
                return target;
            });
        }
    }
}