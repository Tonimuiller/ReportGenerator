namespace TSoft.SHOficina.ReportGenerator.Domain.Entity;

public sealed class Receivable
{
    public int Number { get; set; }
    public string Customer { get; set; } = default!;
    public string? CustomerPhone { get; set; }
    public string? CustomerEmail { get; set; }
    public string AccountPlan { get; set; } = default!;
    public string BillingMethod { get; set; } = default!;
    public DateTime DocumentDate { get; set; }
    public DateTime DocumentDueDate { get; set; }
    public decimal Value { get; set; }
    public bool Payed { get; set; }
    public string? Observations { get; set; }
    public decimal? Fees { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime? PaymentDate { get; set; }

}
