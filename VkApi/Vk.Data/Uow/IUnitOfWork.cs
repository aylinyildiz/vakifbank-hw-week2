using Vk.Data.Domain;
using Vk.Data.Repository;

namespace Vk.Data.Uow;

public interface IUnitOfWork
{
    void Complete();
    void CompleteTransaction();
    
    IGenericRepository<Customer> CustomerRepository { get; }
    IGenericRepository<Account> AccountRepository { get; }
    IGenericRepository<AccountTransaction> AccountTransactionRepository { get; }
    IGenericRepository<Address> AddressRepository { get; }
    IGenericRepository<Card> CardRepository { get; }
    IGenericRepository<EftTransaction>EftTransactionRepository { get; }
}