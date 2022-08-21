using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteItau.Model;

namespace TesteItau.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> Get();

        Task<Item> Get(int Id);

        Task<Item> Create(Item item);

        Task Update(Item item);

        Task Delete(int Id);
    }
}
