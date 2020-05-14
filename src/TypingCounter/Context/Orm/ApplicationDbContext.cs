using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using TypingCounter.Entities;

namespace TypingCounter.Context.Orm
{
    public class ApplicationDbContext : DbContextBase
    {
        private static readonly ILoggerFactory _loggerFactory =
            new ServiceCollection()
            .AddLogging(
                builder => builder.AddProvider(
                    new NLogLoggerProvider()).AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Debug))
            .BuildServiceProvider().GetService<ILoggerFactory>();

        public virtual DbSet<ArchiveDate> ArchiveDate { get; set; }
        public virtual DbSet<Archive> Archive { get; set; }
        public virtual DbSet<Current> Current { get; set; }

        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext([NotNull]DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseLoggerFactory(_loggerFactory)
            //    .EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new SqliteConnectionStringBuilder { DataSource = @"TypingCounter.db" }.ToString();
                optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Archive>()
                .HasKey(e => new { e.ArchiveId, e.Code });
            modelBuilder.Entity<ArchiveDate>()
                .HasMany(e => e.Archives);
        }
    }
}