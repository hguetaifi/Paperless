using Paperless.DataAccessLayer.Entities;
namespace Paperless.DataAccessLayer.Interfaces;

public interface IDocumentRepository : IDisposable
{
    public DocumentDTO GetDocument(int documentID);
    public IEnumerable<DocumentDTO> GetAllDocuments();
    public int CreateDocument(DocumentDTO document);
    public void UpdateDocument(int documentID, string text);
    public void DeleteDocument(int documentID);
    public void Save();
}