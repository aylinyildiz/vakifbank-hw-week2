using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Schema;

namespace Vk.Operation;

public class AccountQueryHandler :
    IRequestHandler<GetAllAccountQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>,
    IRequestHandler<GetAccountByCustomerIdQuery, ApiResponse<List<AccountResponse>>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public AccountQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountQuery request,
        CancellationToken cancellationToken)
    {
        List<Account> list = await dbContext.Set<Account>().Include(x => x.Customer).ToListAsync(cancellationToken);

        List<AccountResponse> mapped = mapper.Map<List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mapped);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        Account? entity = await dbContext.Set<Account>().Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            return new ApiResponse<AccountResponse>("Record not found");
        }

        AccountResponse mapped = mapper.Map<AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAccountByCustomerIdQuery request,
        CancellationToken cancellationToken)
    {
        List<Account> list = await dbContext.Set<Account>().Include(x=> x.Customer).Where(x=> x.CustomerId == request.CustomerId ).ToListAsync(cancellationToken);
        
        List<AccountResponse> mapped = mapper.Map<List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mapped);
    }
}