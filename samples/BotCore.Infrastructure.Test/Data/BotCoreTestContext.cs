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
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<UserCurrencyMapping> UserCurrencyMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CurrencyRate>()
                .HasOne(a => a.From)
                .WithMany(b => b.FromCurrencyRates)
                .HasForeignKey(e => e.FromId);

            builder.Entity<CurrencyRate>()
                .HasOne(a => a.To)
                .WithMany(b => b.ToCurrencyRates)
                .HasForeignKey(e => e.ToId);
            
            builder.Entity<UserCurrencyMapping>()
                .HasKey(x => new { x.UserId, x.CurrencyId });  
            builder.Entity<UserCurrencyMapping>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserCurrencyMapping)
                .HasForeignKey(x => x.UserId);  
            builder.Entity<UserCurrencyMapping>()
                .HasOne(x => x.Currency)
                .WithMany(x => x.UserCurrencyMapping)
                .HasForeignKey(x => x.CurrencyId);
            
        }
    }
}