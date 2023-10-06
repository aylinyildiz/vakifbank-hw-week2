using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Api.Controllers
{
    [Route("vk/api/v1/[controller]")]
    [ApiController]
    public class EftTransferContoller : ControllerBase
    {
        private IMediator mediator;

        public EftTransferContoller(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<ApiResponse<EftTransferResponse>> Post([FromBody] EftTransferRequest request)
        {
            var operation = new CreateEftTransfer(request);
            var result = await mediator.Send(operation);
            return result;
        }

    
        [HttpGet("ByReferenceNumber/{referenceNumber}")]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> Post(string referenceNumber)
        {
            var operation = new GetEftTransferByReference(referenceNumber);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByAccountId/{accountId}")]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> Post(int accountId)
        {
            var operation = new GetEftTransferByAccountId(accountId);
            var result = await mediator.Send(operation);
            return result;
        }

    }
}
