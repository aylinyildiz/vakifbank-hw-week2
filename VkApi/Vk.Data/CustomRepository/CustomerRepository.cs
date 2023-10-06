using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Repository;

namespace Vk.Data.CustomRepository;

public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(VkDbContext dbContext) : base(dbContext)
    {
        
    }
}