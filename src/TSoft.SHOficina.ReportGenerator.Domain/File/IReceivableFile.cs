using TSoft.SHOficina.ReportGenerator.Domain.Entity;
using TSoft.SHOficina.ReportGenerator.Domain.File;

namespace TSoft.SHOficina.ReportGenerator.Domain.FileReader;

public interface IReceivableFile
{
    Task<ReceivableFileReadResult> ReadReceivablesAsync(string filePath, CancellationToken cancellationToken);
    Task<ReceivableFileWriteResult> WriteReceivablesAsync(string filePath, IEnumerable<string[]> fileLines, CancellationToken cancellationToken);
}
