using TSoft.SHOficina.ReportGenerator.Application.Receivable.GenerateReceivableReport;
using TSoft.SHOficina.ReportGenerator.Infrastructure.File;

namespace TSoft.SHOficina.ReportGenerator.Presentation.Factory;

internal static class GenerateReceivableReportUseCaseFactory
{
    public static GenerateReceivableReportUseCase GenerateUseCase() => new GenerateReceivableReportUseCase(
        new XlsxReceivableFile());
}
