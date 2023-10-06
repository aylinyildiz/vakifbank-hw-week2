using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Base.Transaction;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class EftTransferCommandHandler :
    IRequestHandler<CreateEftTransfer, ApiResponse<EftTransferResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    public EftTransferCommandHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    private async Task<ApiResponse<Account>> CheckAccount(int accountId, CancellationToken cancellationToken)
    {
        var account = await dbContext.Set<Account>().Where(x => x.Id == accountId).FirstOrDefaultAsync(cancellationToken);

        if (account == null)
        {
            return new ApiResponse<Account>("Invalid Account");
        }

        if (!account.IsActive)
        {
            return new ApiResponse<Account>("Invalid Account");
        }

        return new ApiResponse<Account>(account);
    }

    public async Task<ApiResponse<EftTransferResponse>> Handle(CreateEftTransfer request, CancellationToken cancellationToken)
    {
        if (request.Model.FromAccountId == request.Model.ToAccountId)
        {
            return new ApiResponse<EftTransferResponse>("Accounts cannot be same");
        }

        string refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();

        var checkFromAccount = await CheckAccount(request.Model.FromAccountId, cancellationToken);
        var checkToAccount = await CheckAccount(request.Model.ToAccountId, cancellationToken);
        if (!checkFromAccount.Success)
        {
            return new ApiResponse<EftTransferResponse>(checkFromAccount.Message);
        }
        if (!checkToAccount.Success)
        {
            return new ApiResponse<EftTransferResponse>(checkToAccount.Message);
        }


        var balanceFrom = await BalanceOperation(request.Model.FromAccountId, request.Model.Amount,
            TransactionDirection.Credit, cancellationToken);
        var balanceTo = await BalanceOperation(request.Model.ToAccountId, request.Model.Amount,
            TransactionDirection.Debit, cancellationToken);

        if (!balanceFrom.Success)
        {
            return new ApiResponse<EftTransferResponse>(balanceFrom.Message);
        }
        if (!balanceTo.Success)
        {
            return new ApiResponse<EftTransferResponse>(balanceTo.Message);
        }

        Account from = checkFromAccount.Response;
        Account to = checkToAccount.Response;

        string txnCode = "Eft";

        EftTransaction transactionFrom = new EftTransaction();
        transactionFrom.TransactionDate = DateTime.UtcNow;
        transactionFrom.AccountId = from.Id;
        transactionFrom.TransactionCode = txnCode;
        transactionFrom.IsActive = true;
        transactionFrom.Description = request.Model.Description;
        transactionFrom.ReferenceNumber = refNumber;
        transactionFrom.UpdateDate = DateTime.UtcNow;
        transactionFrom.InsertDate = DateTime.UtcNow;
        transactionFrom.Status = request.Model.Status;
        transactionFrom.Amount = request.Model.Amount;
        transactionFrom.ReceiverAddress = request.Model.ReceiverAddress;
        transactionFrom.ReceiverName = request.Model.ReceiverName;
        transactionFrom.ReceiverAddressType = request.Model.ReceiverAddressType;
        transactionFrom.ChargeAmount = request.Model.ChargeAmount;


        EftTransaction transactionTo = new EftTransaction();
        transactionTo.TransactionDate = DateTime.UtcNow;
        transactionTo.AccountId = to.Id;
        transactionTo.TransactionCode = txnCode;
        transactionTo.IsActive = true;
        transactionTo.Description = request.Model.Description;
        transactionTo.ReferenceNumber = refNumber;
        transactionTo.TransactionDate = DateTime.UtcNow;
        transactionTo.UpdateDate = DateTime.UtcNow;
        transactionTo.InsertDate = DateTime.UtcNow;
        transactionTo.Status = request.Model.Status;
        transactionTo.Amount = request.Model.Amount;
        transactionTo.ReceiverAddress = request.Model.ReceiverAddress;
        transactionTo.ReceiverName = request.Model.ReceiverName;
        transactionTo.ReceiverAddressType = request.Model.ReceiverAddressType;
        transactionTo.ChargeAmount = request.Model.ChargeAmount;


        await dbContext.Set<EftTransaction>().AddAsync(transactionFrom, cancellationToken);
        await dbContext.Set<EftTransaction>().AddAsync(transactionTo, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);


        var response = mapper.Map<EftTransferResponse>(request.Model);
        response.ReferenceNumber = refNumber;
        response.TransactionCode = txnCode;
        response.TransactionDate = DateTime.UtcNow;

        return new ApiResponse<EftTransferResponse>(response);
    }
    private async Task<ApiResponse> BalanceOperation(int accountId, decimal amount, TransactionDirection direction, CancellationToken cancellationToken)
    {
        var account = await dbContext.Set<Account>().Where(x => x.Id == accountId).FirstOrDefaultAsync(cancellationToken);

        if (direction == TransactionDirection.Credit)
        {
            if (account.Balance < amount)
            {
                return new ApiResponse("Insufficent balance");
            }
            account.Balance -= amount;
        }
        if (direction == TransactionDirection.Debit)
        {
            account.Balance += amount;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}
