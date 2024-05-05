namespace TSoft.SHOficina.ReportGenerator.Application.Receivable.GenerateReceivableReport;

public sealed record GenerateReceivableReportResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
