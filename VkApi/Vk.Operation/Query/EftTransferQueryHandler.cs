using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;


public class EftTransferQueryHandler :
    IRequestHandler<GetEftTransferByReference, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetEftTransferByAccountId, ApiResponse<List<AccountTransactionResponse>>>
{

    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public EftTransferQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetEftTransferByReference request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.ReferenceNumber == request.ReferenceNumber).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetEftTransferByAccountId request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.AccountId == request.AccountId).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mapped);
    }
}