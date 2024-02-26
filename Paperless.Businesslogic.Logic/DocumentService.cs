using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Paperless.Businesslogic.Entities;
using Paperless.Businesslogic.Interfaces;
using Paperless.DataAccessLayer.Entities;
using Paperless.DataAccessLayer.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace Paperless.Businesslogic.Logic;

public class DocumentService : IDocument
{
    private readonly IValidator<DocumentEntity> _validator;
    private IDocumentRepository _repository;
    private ILogger<DocumentService> _logger;
    private IMapper _mapper;
    private IMinioClient _minio;
    public DocumentService(IValidator<DocumentEntity> validator, ILogger<DocumentService> logger, IDocumentRepository repository, IMapper mapper, IMinioClient minio)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(_repository));;
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));;
        _validator = validator ?? throw new ArgumentNullException(nameof(_validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        _minio = minio ?? throw new ArgumentNullException(nameof(_minio));
    }

    public async Task<int> CreateDocument(DocumentEntity documentEntity)
    {
        _logger.LogInformation("Creating Document");
        try
        {
           var valid = _validator.Validate(documentEntity);
           if (!valid.IsValid)
           {
               _logger.LogInformation("Document is invalid");
               throw new Exception("Document is invalid");
           }

           if(documentEntity.UploadDocument == null)
           {
               _logger.LogInformation("No Content found");
               throw new Exception("No Content found");
           }
           DocumentDTO documentDto = _mapper.Map<DocumentDTO>(documentEntity);
           if (documentEntity.UploadDocument.FileName != null) documentDto.Title = documentEntity.Title;
           int id = _repository.CreateDocument(documentDto);
           string fileId =  Guid.NewGuid().ToString(); //unique identifier
           await SaveFile(documentEntity.UploadDocument,fileId);
           _logger.LogInformation($"Document created: {fileId}");
           return id; // Indicate success
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create document");
            throw new Exception($"{ex}");
        }
    }
    
    private protected async Task SaveFile(IFormFile file, string fileId)
    {
        try
        {
            using var streamReader = new StreamReader(file.OpenReadStream());
            var memoryStream = new MemoryStream();
            await streamReader.BaseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var putObjectArgs = new PutObjectArgs()
                .WithBucket("paperless-bucket")
                .WithObject(fileId)
                .WithContentType(file.ContentType)
                .WithStreamData(memoryStream)
                .WithObjectSize(memoryStream.Length);

            await _minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError($"Minio Error: {e.Message}");
        }
    }
    
    public async Task<DocumentEntity> GetDocument(int id)
    {
        _logger.LogInformation($"Retreive Document [id:{id}]");
        try
        {
            if(id == 0)
            {
                _logger.LogInformation("Not a valid Id found");
                throw new Exception("Id cant be null");
            }

           DocumentDTO documentDto = _repository.GetDocument(id);
           DocumentEntity documentEntity = _mapper.Map<DocumentEntity>(documentDto);
            
           return documentEntity; // Indicate success
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get document");
            throw new Exception($"{ex}"); // Indicate failure
        }
    }

}