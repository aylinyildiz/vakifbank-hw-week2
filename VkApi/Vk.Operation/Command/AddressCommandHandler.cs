using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;


public class AddressCommandHandler :
    IRequestHandler<CreateAddressCommand, ApiResponse<AddressResponse>>,
    IRequestHandler<UpdateAddressCommand, ApiResponse>,
    IRequestHandler<DeleteAddressCommand, ApiResponse>

{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public AddressCommandHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        Address mapped = mapper.Map<Address>(request.Model);

        var entity = await dbContext.Set<Address>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<AddressResponse>(entity.Entity);
        return new ApiResponse<AddressResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Address>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        entity.AddressLine1 = request.Model.AddressLine1;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Address>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
