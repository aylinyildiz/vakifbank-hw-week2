using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation;

public record CreateCustomerCommand(CustomerRequest Model) : IRequest<ApiResponse<CustomerResponse>>;
public record UpdateCustomerCommand(CustomerRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(int Id) : IRequest<ApiResponse>;
public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerByIdQuery(int Id) : IRequest<ApiResponse<CustomerResponse>>;