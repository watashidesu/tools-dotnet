using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using TypingCounter.Context.Orm;
using TypingCounter.Entities;

namespace TypingCounter.Services
{
    /// <summary>
    /// アーカイブに関する操作です。
    /// </summary>
    public class ArchiveService : ServiceBase
    {
        /// <summary>
        /// アーカイブを全件取得します。
        /// </summary>
        public List<ArchiveDate> FindAll()
        {
            using var context = new ApplicationDbContext();
            return context.ArchiveDate.Include(nameof(ArchiveDate.Archives)).ToList();
        }

        /// <summary>
        /// タイプ数をアーカイブします。
        /// 初期タイプ数リストを返却します。
        /// </summary>
        public List<Current> Archive()
        {
            using var context = new ApplicationDbContext();
            var nowDate = DateTime.Now.Date;

            return context.Tx(() =>
            {
                var keys = context.Current.ToList();
                var mod = context.ArchiveDate.Include(nameof(ArchiveDate.Archives)).SingleOrDefault(ad => ad.DateTime.Date == nowDate.Date);
                // insert
                if (mod == null)
                {
                    var archives = keys.Select(k => new Archive() { Code = k.Code, Count = k.Count }).ToList();
                    var reg = new ArchiveDate() { DateTime = nowDate, Archives = archives };
                    context.ArchiveDate.Add(reg);
                }
                // update
                else
                {
                    foreach (var modKey in mod.Archives)
                    {
                        modKey.Count += keys.SingleOrDefault(k => k.Code == modKey.Code)?.Count ?? 0;
                    }
                }

                context.RemoveRange(context.Current);

                var regKey = Current.CreateInitialKeyList();
                context.Current.AddRange(regKey);
                context.SaveChanges();
                return regKey;
            });
        }
    }
}