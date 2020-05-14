using Microsoft.EntityFrameworkCore;
using System;
using TypingCounter.Context.Orm;

namespace TypingCounter.Test
{
    public class TestBase
    {
        public ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}