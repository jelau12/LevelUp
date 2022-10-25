using LevelUp.DataAccess.Base;
using System;
using LevelUp.Entities.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LevelUp.DataAccess
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Set<Product>().ToListAsync();
        }
        public override async Task<Product> GetByIdAsync(int? id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
