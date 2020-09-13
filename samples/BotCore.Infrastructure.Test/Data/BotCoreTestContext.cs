using System.Reflection;
using BotCore.Core.Test.Entities;
using Microsoft.EntityFrameworkCore;

namespace BotCore.Infrastructure.Test.Data
{
    public class BotCoreTestContext : DbContext
    {
        public BotCoreTestContext(DbContextOptions<BotCoreTestContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currency { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}