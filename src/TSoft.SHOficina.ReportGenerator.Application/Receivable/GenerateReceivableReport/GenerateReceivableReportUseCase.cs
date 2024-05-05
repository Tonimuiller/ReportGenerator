using TSoft.SHOficina.ReportGenerator.Domain.FileReader;

namespace TSoft.SHOficina.ReportGenerator.Application.Receivable.GenerateReceivableReport;

public sealed class GenerateReceivableReportUseCase
{
    private IReceivableFile _receivableFile;

    public GenerateReceivableReportUseCase(IReceivableFile receivableFile)
    {
        ArgumentNullException.ThrowIfNull(receivableFile);
        _receivableFile = receivableFile;
    }

    public async Task<GenerateReceivableReportResponse> ExecuteAsync(GenerateReceivableReportRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var receivableFileReadResult = await _receivableFile.ReadReceivablesAsync(request.InputFilePath, cancellationToken);
        if (!receivableFileReadResult.Success)
        {
            return new GenerateReceivableReportResponse
            {
                IsSuccess = false,
                Message = receivableFileReadResult.Message
            };
        }

        if (!(receivableFileReadResult.Receivables?.Any() ?? false))
        {
            return new GenerateReceivableReportResponse
            {
                IsSuccess = false,
                Message = "O arquivo não contém registros de contas à receber."
            };
        }

        var receivableCustomerGrouped = receivableFileReadResult.Receivables!
            .GroupBy(receivables => receivables.Customer)
            .OrderBy(g => g.Key)
            .ToList();

        var fileLines = new List<string[]>
        {
            new string[]
            {
                "Nº Fatura",
                "Cliente",
                "Telefone",
                "Email",
                "Pl.Contas",
                "Cobrança",
                "Dt.Docto",
                "Dt.Vencto",
                "Valor",
                "Pg?",
                "Observações",
                "Juros",
                "Total",
                "Dt.Pgto"
            }
        };

        fileLines.Add(GenerateStringArray(fileLines.First().Length));

        foreach(var customerReceivables in receivableCustomerGrouped)
        {
            var receivables = customerReceivables
                .OrderBy(receivable => receivable.DocumentDueDate)
                .Select(receivable => new string[]
                {
                    receivable.Number.ToString(),
                    receivable.Customer,
                    receivable.CustomerPhone ?? string.Empty,
                    receivable.CustomerEmail ?? string.Empty,
                    receivable.AccountPlan,
                    receivable.BillingMethod,
                    receivable.DocumentDate.ToString("dd/MM/yyyy HH:mm:ss"),
                    receivable.DocumentDueDate.ToString("dd/MM/yyyy HH:mm:ss"),
                    receivable.Value.ToString("0.00"),
                    receivable.Payed ? "Sim" : "Não",
                    receivable.Observations ?? string.Empty,
                    receivable.Fees?.ToString("0.00") ?? string.Empty,
                    receivable.TotalValue.ToString("0.00"),
                    receivable.PaymentDate?.ToString("dd/MM/yyyy HH:mm:ss") ?? string.Empty
                })
                .ToList();
            var summaryLine = GenerateStringArray(receivables.First().Length);
            summaryLine[8] = customerReceivables.Sum(c => c.Value).ToString("0.00");
            summaryLine[11] = customerReceivables.Sum(c => c.Fees)?.ToString("0.00") ?? string.Empty;
            summaryLine[12] = customerReceivables.Sum(c => c.TotalValue).ToString("0.00");
            receivables.Add(summaryLine);
            receivables.Add(GenerateStringArray(receivables.First().Length));

            fileLines.AddRange(receivables);
        }

        var receivableFileWriteResponse = await _receivableFile.WriteReceivablesAsync(
            request.OutputFilePath,
            fileLines, 
            cancellationToken);
        return new GenerateReceivableReportResponse
        {
            IsSuccess = receivableFileWriteResponse.Success,
            Message = receivableFileWriteResponse.Message
        };
    }

    private string[] GenerateStringArray(int length) => new string[length];
}
