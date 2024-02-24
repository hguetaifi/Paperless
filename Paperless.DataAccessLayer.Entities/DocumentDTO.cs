namespace Paperless.DataAccessLayer.Entities;

public class DocumentDTO
{
    public int Id { get; set; }
    public int? Correspondent { get; set; }
    public int? DocumentType { get; set; }
    public int? StoragePath { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? Modified { get; set; }
    public DateTime? Added { get; set; }
    public string? ArchiveSerialNumber { get; set; }
    public string? OriginalFileName { get; set; }
    public string? ArchivedFileName { get; set; }
}