using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotCore.Core.CurrencyBot.Entities;
using BotCore.Core.CurrencyBot.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BotCore.Infrastructure.CurrencyBot.Data
{
    public class GenericRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly BotCoreTestContext DbContext;

        public GenericRepository(BotCoreTestContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> FirstAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await DbContext.Set<T>().AddRangeAsync(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            await DbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(DbContext.Set<T>().AsQueryable(), spec);
        }
    }
}