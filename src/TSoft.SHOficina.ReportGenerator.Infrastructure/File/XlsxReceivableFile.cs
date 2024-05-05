using OfficeOpenXml;
using TSoft.SHOficina.ReportGenerator.Domain.Entity;
using TSoft.SHOficina.ReportGenerator.Domain.File;
using TSoft.SHOficina.ReportGenerator.Domain.FileReader;

namespace TSoft.SHOficina.ReportGenerator.Infrastructure.File;

public sealed class XlsxReceivableFile : IReceivableFile
{
    public XlsxReceivableFile()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;    
    }

    public async Task<ReceivableFileReadResult> ReadReceivablesAsync(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            using var excelPackage = new ExcelPackage();
            await excelPackage.LoadAsync(filePath, cancellationToken);
            var workSheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
            var receivables = new List<Receivable>();
            for (int rowIndex = 4; rowIndex <= workSheet!.Rows.Count(); rowIndex++)
            {
                receivables.Add(new Receivable
                {
                    Number = int.Parse(workSheet.Cells[rowIndex, 1].Value.ToString()!),
                    Customer = workSheet.Cells[rowIndex, 2].Value.ToString()!,
                    CustomerPhone = workSheet.Cells[rowIndex, 3].Value?.ToString(),
                    CustomerEmail = workSheet.Cells[rowIndex, 4].Value?.ToString(),
                    AccountPlan = workSheet.Cells[rowIndex, 5].Value.ToString()!,
                    BillingMethod = workSheet.Cells[rowIndex, 6].Value.ToString()!,
                    DocumentDate = DateTime.Parse(workSheet.Cells[rowIndex, 7].Value.ToString()!),
                    DocumentDueDate = DateTime.Parse(workSheet.Cells[rowIndex, 8].Value.ToString()!),
                    Value = decimal.Parse(workSheet.Cells[rowIndex, 9].Value.ToString()!),
                    Payed = bool.Parse(workSheet.Cells[rowIndex, 10].Value.ToString()!),
                    Observations = workSheet.Cells[rowIndex, 11].Value?.ToString(),
                    Fees = !string.IsNullOrEmpty(workSheet.Cells[rowIndex, 12].Value?.ToString())
                        ? decimal.Parse(workSheet.Cells[rowIndex, 12].Value.ToString()!)
                        : null,
                    TotalValue = decimal.Parse(workSheet.Cells[rowIndex, 13].Value.ToString()!),
                    PaymentDate = !string.IsNullOrEmpty(workSheet.Cells[rowIndex, 14].Value?.ToString())
                        ? DateTime.Parse(workSheet.Cells[rowIndex, 14].Value.ToString()!)
                        : null
                });
            }

            return new ReceivableFileReadResult(true, receivables);
        }
        catch (Exception ex)
        {
            return new ReceivableFileReadResult(
                false,
                message: $"Erro ao ler o arquivo de origem: ${ex.Message}");
        }
    }

    public async Task<ReceivableFileWriteResult> WriteReceivablesAsync(
        string filePath,
        IEnumerable<string[]> fileLines, 
        CancellationToken cancellationToken)
    {
        try
        {
            using var excelPackage = new ExcelPackage();
            var workSheet = excelPackage.Workbook.Worksheets.Add("Contas à receber");
            var rowIndex = 1;
            foreach (var fileLine in fileLines)
            {
                var colIndex = 1;
                foreach (var cellValue in fileLine)
                {
                    if (decimal.TryParse(cellValue, out var decimalCellValue))
                    {
                        workSheet.Cells[rowIndex, colIndex].Style.Numberformat.Format = "0.00";
                        workSheet.Cells[rowIndex, colIndex++].Value = decimalCellValue;
                    }
                    else
                    {
                        workSheet.Cells[rowIndex, colIndex++].Value = cellValue;
                    }
                }

                rowIndex++;
            }

            await excelPackage.SaveAsAsync(filePath, cancellationToken);

            return new ReceivableFileWriteResult(true);
        }
        catch (Exception ex)
        {
            return new ReceivableFileWriteResult(
                false,
                message: $"Erro ao salvar o arquivo de destino: ${ex.Message}");
        }
    }
}
