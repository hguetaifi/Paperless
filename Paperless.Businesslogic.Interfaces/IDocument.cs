using Microsoft.AspNetCore.Mvc;
using Paperless.Businesslogic.Entities;

namespace Paperless.Businesslogic.Interfaces
{
    public interface IDocument
    {
       public Task<bool> CreateDocument(DocumentEntity documentEntity);
       public DocumentEntity GetDocument(Guid id);
    }
}

