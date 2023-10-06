namespace Vk.Schema;

public class CardRequest
{
    public int AccountId { get; set; }
    public string CardHolder { get; set; }
    public long CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public string ExpiryDate { get; set; } // DDyy
    public int? ExpenseLimit { get; set; }
}

public class CardResponse
{
    public int AccountId { get; set; }
    public string CardHolder { get; set; }
    public long CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public string ExpiryDate { get; set; } // DDyy
    public int? ExpenseLimit { get; set; }
    public string AccountName { get; set; }
    public int AccountNumber { get; set; }
}