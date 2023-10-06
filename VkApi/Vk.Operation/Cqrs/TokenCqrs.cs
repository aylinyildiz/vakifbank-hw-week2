using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;