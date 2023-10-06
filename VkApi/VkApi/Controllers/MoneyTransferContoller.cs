using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace VkApi.Controllers;


[Route("vk/api/v1/[controller]")]
[ApiController]
public class MoneyTransferContoller : ControllerBase
{
    private IMediator mediator;

    public MoneyTransferContoller(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    
    [HttpPost]
    public async Task<ApiResponse<MoneyTransferResponse>> Post([FromBody] MoneyTransferRequest request)
    {
        var operation = new CreateMoneyTransfer(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("EftTransfer")]
    public async Task<ApiResponse<EftTransferResponse>> Post([FromBody] EftTransferRequest request)
    {
        var operation = new CreateEftTransfer(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByReferenceNumber/{referenceNumber}")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Post(string referenceNumber)
    {
        var operation = new GetMoneyTransferByReference(referenceNumber);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("ByAccountId/{accountId}")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Post(int accountId)
    {
        var operation = new GetMoneyTransferByAccountId(accountId);
        var result = await mediator.Send(operation);
        return result;
    }
    
}