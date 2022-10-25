using LevelUp.Entities.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelUp.DataAccess.Base
{
    public interface IRepositoryBase<T>
    {
        LevelUpDbContext Context { get; set; }

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task CreateAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);
    }
}
