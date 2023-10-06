using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record CreateEftTransfer(EftTransferRequest Model) : IRequest<ApiResponse<EftTransferResponse>>;
public record GetEftTransferByReference(string ReferenceNumber) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
public record GetEftTransferByAccountId(int AccountId) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
