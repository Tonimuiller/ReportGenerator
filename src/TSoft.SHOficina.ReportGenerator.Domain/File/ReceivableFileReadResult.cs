using TSoft.SHOficina.ReportGenerator.Domain.Entity;

namespace TSoft.SHOficina.ReportGenerator.Domain.File;

public sealed class ReceivableFileReadResult : FileOperationResult
{
    public ReceivableFileReadResult(
        bool success,
        IEnumerable<Receivable>? receivables = null,
        string? message = null) : base (success, message)
    {
        Receivables = receivables;
    }

    public IEnumerable<Receivable>? Receivables { get; }
}
