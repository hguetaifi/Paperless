using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Paperless.Businesslogic.Entities;

namespace Paperless.Businesslogic.Interfaces
{
    public interface IDocument
    {
       public Task<IActionResult> CreateDocument(DocumentEntity document);
       public Document GetDocument(Guid id);
    }
}

