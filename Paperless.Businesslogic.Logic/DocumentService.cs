using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paperless.Businesslogic.Entities;
using Paperless.Businesslogic.Interfaces;
using Paperless.DataAccessLayer.Entities;
using Paperless.DataAccessLayer.Interfaces;
using Paperless.DataAccessLayer.Sql;

namespace Paperless.Businesslogic.Logic;

public class DocumentService : IDocument
{
    private readonly IValidator<DocumentEntity> _validator;
    private IDocumentRepository _repository;
    private ILogger<DocumentService> _logger;
    private IMapper _mapper;
    public DocumentService(IValidator<DocumentEntity> validator, ILogger<DocumentService> logger, IDocumentRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(_repository));;
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));;
        _validator = validator ?? throw new ArgumentNullException(nameof(_validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }

    public async Task<bool> CreateDocument(DocumentEntity documentEntity)
    {
        _logger.LogInformation("Creating Document");
        try
        {
           var valid = _validator.Validate(documentEntity);
           if (!valid.IsValid)
           {
               _logger.LogInformation("Document is invalid");
               return false;
           }

           if(documentEntity.UploadDocument == null)
           {
               _logger.LogInformation("No Content found");
               return false;
           }
           DocumentDTO documentDto = _mapper.Map<DocumentDTO>(documentEntity);
           documentDto.Title = documentEntity.UploadDocument.FileName;
           int id = _repository.CreateDocument(documentDto);
           _logger.LogInformation("Document created");
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