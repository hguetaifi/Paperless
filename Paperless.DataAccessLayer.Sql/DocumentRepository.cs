using Paperless.DataAccessLayer.Entities;
using Paperless.DataAccessLayer.Interfaces;

namespace Paperless.DataAccessLayer.Sql;

public class DocumentRepository : IDocumentRepository
{
    private DBContext _db;
    private bool _disposed = false;

    public DocumentRepository()
    {
        _db = new DBContext();
    }
    public DocumentRepository(DBContext db)
    {
        _db = db;
    }

    public DocumentDTO GetDocument(int documentId)
    {
        return _db.Documents.Find(documentId)!;
    }


    public IEnumerable<DocumentDTO> GetAllDocuments()
    {
        return _db.Documents.ToList();
    }

    public int CreateDocument(DocumentDTO document)
    {
        _db.Documents.Add(document);
        Save();
        return document.Id;
    }

    public void UpdateDocument(int documentId, string text)
    {
        var document = _db.Documents.Find(documentId);
        document!.Content = text;
        _db.Documents.Entry(document).Property(x => x.Content).IsModified = true;
        Save();
    }

    public void DeleteDocument(int documentId)
    {
        var document = _db.Documents.Find(documentId);
        _db.Documents.Remove(document!);
        Save();
    }
    
    public void Save(){        
        _db.Database.EnsureCreated();
        _db.SaveChanges();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing) _db.Dispose();
        }
        _disposed = true;
    }
}