namespace Vk.Schema;

public class AccountTransactionRequest
{
    public int AccountId { get; set; }
    public string ReferenceNumber { get; set; }   
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
}

public class AccountTransactionResponse
{
    public int AccountId { get; set; }
    public string ReferenceNumber { get; set; }   
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
    public string AccountName { get; set; }
    public int AccountNumber { get; set; }
    public int CustomerNumber { get; set; }
    public string CustomerName { get; set; }
}