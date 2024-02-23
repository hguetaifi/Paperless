using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paperless.Businesslogic.Entities;
using Paperless.Businesslogic.Interfaces;
namespace Paperless.Businesslogic.Logic;

public class DocumentService : IDocument
{
    private readonly IValidator _validator;
    private ILogger<DocumentService> _logger;
    public DocumentService(IValidator<DocumentEntity> validator, ILogger<DocumentService> logger)
    {
        _logger = logger;
        _validator = validator;
    }

    public async Task<bool> CreateDocument(DocumentEntity documentEntity)
    {
        try
        {
            _logger.LogInformation("Document created");
            //TODO: map to DAL and persist in DB
            // e.g., _repository.Add(documentEntity);
            // await _dbContext.SaveChangesAsync();

            return true; // Indicate success
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create document");
            return false; // Indicate failure
        }
    }

    public DocumentEntity GetDocument(Guid id)
    {
        return new DocumentEntity();
    }

}