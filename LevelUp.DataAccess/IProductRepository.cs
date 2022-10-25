using LevelUp.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelUp.DataAccess
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int? id);
    }
}