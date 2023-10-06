namespace Vk.Schema;


public class EftTransferRequest
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverAddress { get; set; }
    public string ReceiverAddressType { get; set; }
    public int Status { get; set; }

    public decimal ChargeAmount { get; set; }
}

public class EftTransferResponse
{
    public int AccountId { get; set; }
    public string ReferenceNumber { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverAddress { get; set; }
    public string ReceiverAddressType { get; set; }
    public decimal Amount { get; set; }
    public decimal ChargeAmount { get; set; }
    public string Description { get; set; }
    public string TransactionCode { get; set; }
    public DateTime TransactionDate { get; set; }
    public int Status { get; set; }
    public string AccountName { get; set; }
    public int AccountNumber { get; set; }
    public int CustomerNumber { get; set; }
    public string CustomerName { get; set; }
}

