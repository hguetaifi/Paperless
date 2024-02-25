using Microsoft.AspNetCore.Mvc;
using Paperless.Businesslogic.Entities;

namespace Paperless.Businesslogic.Interfaces
{
    public interface IDocument
    {
       public Task<int> CreateDocument(DocumentEntity documentEntity);
       public Task<DocumentEntity> GetDocument(int id);
    }
}

