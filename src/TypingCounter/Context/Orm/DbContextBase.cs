using Core.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using TypingCounter.Entities;

namespace TypingCounter.Context.Orm
{
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase() : base()
        {
        }

        public DbContextBase([NotNull]DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// トランザクション処理を実行します。
        /// </summary>
        /// <param name="action">実行内容</param>
        public void Tx(Action action)
        {
            using var transaction = Database.BeginTransaction();
            try
            {
                action();
                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex)
                {
                    Logger.Error("ロールバックに失敗しました。", ex);
                }

                throw;
            }
        }

        /// <summary>
        /// 戻り値を持つトランザクション処理を実行します。
        /// </summary>
        /// <param name="func">実行内容</param>
        /// <returns>戻り値</returns>
        public TResult Tx<TResult>(Func<TResult> func)
        {
            using var transaction = Database.BeginTransaction();
            TResult result;
            try
            {
                result = func();
                transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex)
                {
                    Logger.Error("ロールバックに失敗しました。", ex);
                }

                throw;
            }

            return result;
        }

        /// <summary>
        /// 更新または新規登録を行います。
        /// </summary>
        public void InsertOrUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            if (Entry(entity).State == EntityState.Detached)
                Add(entity);
            else
                Update(entity);
        }

        /// <summary>
        /// 複数件の更新または新規登録を行います。
        /// </summary>
        public void InsertOrUpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                InsertOrUpdate(entity);
            }
        }

        /// <summary>
        /// 変更を確定します。
        /// </summary>
        public override int SaveChanges()
        {
            var objectStateEntries =
                ChangeTracker.Entries().Where(e => e.Entity is EntityBase && (e.State == EntityState.Modified || e.State == EntityState.Added)).ToList();
            var currentTime = DateTime.Now;
            foreach (var entry in objectStateEntries)
            {
                if (!(entry.Entity is EntityBase entityBase))
                    continue;
                if (entry.State == EntityState.Added)
                    entityBase.CreatedDateTime = currentTime;
                entityBase.LastModifiedDateTime = currentTime;
            }
            return base.SaveChanges();
        }
    }
}