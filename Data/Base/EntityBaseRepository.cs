using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booking_Hotel.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext context;
        public EntityBaseRepository(AppDbContext _context)
        {
            this.context = _context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task InsertAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entry)
        {
            EntityEntry entryEntry = context.Entry<T>(entry);
            entryEntry.State = EntityState.Modified;
            await context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var entry = await GetByIdAsync(id);
            EntityEntry entryEntry = context.Entry<T>(entry);
            entryEntry.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
