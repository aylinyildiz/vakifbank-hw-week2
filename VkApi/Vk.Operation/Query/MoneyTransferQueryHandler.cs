using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Schema;
namespace Vk.Operation;

public class MoneyTransferQueryHandler :
    IRequestHandler<GetMoneyTransferByReference, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetMoneyTransferByAccountId, ApiResponse<List<AccountTransactionResponse>>>
{
    
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public MoneyTransferQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetMoneyTransferByReference request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.ReferenceNumber == request.ReferenceNumber).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetMoneyTransferByAccountId request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.AccountId == request.AccountId).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
    }
}