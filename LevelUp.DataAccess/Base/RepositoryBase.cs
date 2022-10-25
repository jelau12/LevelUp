using LevelUp.Entities.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelUp.DataAccess.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Database context
        /// </summary>
        private protected LevelUpDbContext _context;

        /// <summary>
        /// Sets the context to provided parameter item
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(LevelUpDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Initialize the context
        /// </summary>
        public RepositoryBase()
        {
            _context = new LevelUpDbContext();
        }

        public virtual LevelUpDbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// gets a item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Creates a item
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task CreateAsync(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a item
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(T t)
        {
            _context.Set<T>().Update(t);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a item
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(T t)
        {
            _context.Set<T>().Remove(t);
            await _context.SaveChangesAsync();
        }
    }
}