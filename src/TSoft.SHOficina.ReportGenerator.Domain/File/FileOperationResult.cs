namespace TSoft.SHOficina.ReportGenerator.Domain.File;

public abstract class FileOperationResult
{
    protected FileOperationResult(
        bool success,
        string? message = null)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; }

    public string? Message { get; }
}
