using Microsoft.EntityFrameworkCore;
using SMN_Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Infrastructure.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly BlogDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public EFRepository(BlogDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task Create(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context?.Dispose();

        public virtual async Task<TEntity> FindAsync(int Id) => await DbSet.FindAsync(Id);
        public virtual async Task<IEnumerable<TEntity>> GetAll() => await DbSet.ToListAsync();
        public virtual async Task<int> SaveChangeAsync() => await _context.SaveChangesAsync();
        public virtual async Task Update(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
       
    }
}
