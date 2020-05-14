using System;
using System.Linq;
using System.Windows.Forms;
using TypingCounter.Entities;
using Xunit;

namespace TypingCounter.Test.Context.Orm
{
    public class DbContextTest : TestBase
    {
        [Fact]
        public void AutoTimeStampUpdateTest()
        {
            using var context = CreateDbContext();
            var key = new Current { Code = Keys.Control, Count = 12 };
            context.Add(key);
            var t1 = DateTime.Now;
            context.SaveChanges();
            var t2 = DateTime.Now;
            Assert.True(t1 < key.CreatedDateTime && t2 > key.CreatedDateTime);
            Assert.True(t1 < key.LastModifiedDateTime && t2 > key.LastModifiedDateTime);

            key = context.Current.Last();
            key.Count++;
            context.SaveChanges();
            var t3 = DateTime.Now;
            Assert.True(t1 < key.CreatedDateTime && t2 > key.CreatedDateTime);
            Assert.True(t1 < key.LastModifiedDateTime && t2 < key.LastModifiedDateTime && t3 > key.LastModifiedDateTime);
        }
    }
}
