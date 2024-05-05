namespace TSoft.SHOficina.ReportGenerator.Domain.File;

public sealed class ReceivableFileWriteResult : FileOperationResult
{
    public ReceivableFileWriteResult(
        bool success, 
        string? message = null) : base(success, message)
    {
    }
}
