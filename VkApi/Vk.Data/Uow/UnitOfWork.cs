using Serilog;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Repository;

namespace Vk.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly VkDbContext dbContext;

    public UnitOfWork(VkDbContext dbContext)
    {
        this.dbContext = dbContext;

        CustomerRepository = new GenericRepository<Customer>(dbContext);
        AddressRepository = new GenericRepository<Address>(dbContext);
        AccountTransactionRepository = new GenericRepository<AccountTransaction>(dbContext);
        AccountRepository = new GenericRepository<Account>(dbContext);
        CardRepository = new GenericRepository<Card>(dbContext);
        EftTransactionRepository = new GenericRepository<EftTransaction>(dbContext);
    }

    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void CompleteTransaction()
    {
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Log.Error("CompleteTransactionError", ex);
            }
        }
    }


    public IGenericRepository<Customer> CustomerRepository { get; private set; }
    public IGenericRepository<Account> AccountRepository { get; private set; }
    public IGenericRepository<AccountTransaction> AccountTransactionRepository { get; private set; }
    public IGenericRepository<Address> AddressRepository { get; private set; }
    public IGenericRepository<Card> CardRepository { get; private set; }
    public IGenericRepository<EftTransaction> EftTransactionRepository { get; private set; }
}