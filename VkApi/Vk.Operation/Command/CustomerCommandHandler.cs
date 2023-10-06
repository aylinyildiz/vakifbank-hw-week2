using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Command;

public class CustomerCommandHandler : 
    IRequestHandler<CreateCustomerCommand,ApiResponse<CustomerResponse>>,
    IRequestHandler<UpdateCustomerCommand,ApiResponse>,
    IRequestHandler<DeleteCustomerCommand,ApiResponse>
    
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerCommandHandler(VkDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    
    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer mapped = mapper.Map<Customer>(request.Model);
        
        var entity = await dbContext.Set<Customer>().AddAsync(mapped,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CustomerResponse>(entity.Entity);
        return new ApiResponse<CustomerResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
       var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
       if (entity == null)
       {
           return new ApiResponse("Record not found!");
       }
       entity.FirstName = request.Model.FirstName;
       entity.LastName = request.Model.LastName;

       await dbContext.SaveChangesAsync(cancellationToken);
       return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}