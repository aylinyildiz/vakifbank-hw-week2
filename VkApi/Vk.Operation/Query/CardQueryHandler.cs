using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class CardQueryHandler :
    IRequestHandler<GetAllCardQuery, ApiResponse<List<CardResponse>>>,
    IRequestHandler<GetCardByIdQuery, ApiResponse<CardResponse>>,
    IRequestHandler<GetCardByAccountIdQuery, ApiResponse<List<CardResponse>>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public CardQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<CardResponse>>> Handle(GetAllCardQuery request,
        CancellationToken cancellationToken)
    {
        List<Card> list = await dbContext.Set<Card>().Include(x => x.Account).ToListAsync(cancellationToken);

        List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);
        return new ApiResponse<List<CardResponse>>(mapped);
    }

    public async Task<ApiResponse<CardResponse>> Handle(GetCardByIdQuery request,
        CancellationToken cancellationToken)
    {
        Card? entity = await dbContext.Set<Card>().Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            return new ApiResponse<CardResponse>("Record not found");
        }

        CardResponse mapped = mapper.Map<CardResponse>(entity);
        return new ApiResponse<CardResponse>(mapped);
    }

    public async Task<ApiResponse<List<CardResponse>>> Handle(GetCardByAccountIdQuery request,
        CancellationToken cancellationToken)
    {
        List<Card> list = await dbContext.Set<Card>().Include(x => x.Account).Where(x => x.AccountId == request.AccountId).ToListAsync(cancellationToken);

        List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);
        return new ApiResponse<List<CardResponse>>(mapped);
    }

}