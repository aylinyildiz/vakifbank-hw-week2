namespace Vk.Schema;

public class EftTransactionRequest
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
}

public class EftTransactionResponse
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