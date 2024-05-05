namespace TSoft.SHOficina.ReportGenerator.Application.Receivable.GenerateReceivableReport;

public sealed record GenerateReceivableReportRequest
{
    public string InputFilePath { get; set; } = default!;
    public string OutputFilePath { get; set; } = default!;
}
