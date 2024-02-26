using Moq;
using FluentValidation;
using Paperless.Businesslogic.Entities;
using Paperless.Businesslogic.Logic;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio;
using Paperless.DataAccessLayer.Entities;
using Paperless.DataAccessLayer.Interfaces;

namespace Paperless.UnitTests;

[TestFixture]
public class DocumentServiceTests
{
    private Mock<IValidator<DocumentEntity>> _validatorMock;
    private Mock<IDocumentRepository> _repositoryMock;
    private Mock<ILogger<DocumentService>> _loggerMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IMinioClient> _minIOMock;
    private DocumentService _service;

    [SetUp]
    public void Setup()
    {
        _validatorMock = new Mock<IValidator<DocumentEntity>>();
        _repositoryMock = new Mock<IDocumentRepository>();
        _loggerMock = new Mock<ILogger<DocumentService>>();
        _mapperMock = new Mock<IMapper>();
        _minIOMock = new Mock<IMinioClient>();
        _service = new DocumentService(_validatorMock.Object, _loggerMock.Object, _repositoryMock.Object, _mapperMock.Object, _minIOMock.Object);
    }

    [Test]
    public async Task GetDocument_Should_Not_Return_Null()
    {
        var documentId = 1;
        var documentDto = new DocumentDTO
        {   Id = 1, 
            Correspondent = 2, 
            DocumentType = 3, 
            Title = "Beispiel Titel",
            Content = "Beispielinhalt", 
            Created = DateTime.Now, 
            Modified = DateTime.Now,
            ArchiveSerialNumber = "123456",
            OriginalFileName = "original.pdf",
            ArchivedFileName = "archived.pdf",
        };
        var documentEntity = new DocumentEntity { 
            Id = 1, 
            Correspondent = 2, 
            DocumentType = 3, 
            Title = "Beispiel Titel",
            Content = "Beispielinhalt",
            Tags = new List<int> { 1, 2, 3 }, 
            Created = DateTime.Now, 
            Modified = DateTime.Now,
            ArchiveSerialNumber = "123456",
            OriginalFileName = "original.pdf",
            ArchivedFileName = "archived.pdf",
        };
        _repositoryMock.Setup(r => r.GetDocument(documentId)).Returns(documentDto);
        _mapperMock.Setup(m => m.Map<DocumentEntity>(It.IsAny<DocumentDTO>())).Returns(documentEntity);

        var result = await _service.GetDocument(documentId);

        Assert.IsNotNull(result);
        _repositoryMock.Verify(r => r.GetDocument(documentId), Times.Once);
    }
    
    [Test]
    public async Task GetDocument_Should_Return_Document_When_Found()
    {
    var documentId = 1;
    var expectedDocumentDto = new DocumentDTO
    {   
        Id = 1, 
        Correspondent = 2, 
        DocumentType = 3, 
        Title = "Beispiel Titel",
        Content = "Beispielinhalt", 
        Created = DateTime.Now, 
        Modified = DateTime.Now,
        ArchiveSerialNumber = "123456",
        OriginalFileName = "original.pdf",
        ArchivedFileName = "archived.pdf",
    };
    var expectedDocumentEntity = new DocumentEntity { 
        Id = 1, 
        Correspondent = 2, 
        DocumentType = 3, 
        Title = "Beispiel Titel",
        Content = "Beispielinhalt",
        Tags = new List<int> { 1, 2, 3 }, 
        Created = DateTime.Now, 
        Modified = DateTime.Now,
        ArchiveSerialNumber = "123456",
        OriginalFileName = "original.pdf",
        ArchivedFileName = "archived.pdf",
    };
    _repositoryMock.Setup(r => r.GetDocument(documentId)).Returns(expectedDocumentDto);
    _mapperMock.Setup(m => m.Map<DocumentEntity>(It.IsAny<DocumentDTO>())).Returns(expectedDocumentEntity);

    var result = await _service.GetDocument(documentId);

    Assert.IsNotNull(result);
    Assert.AreEqual(expectedDocumentEntity.Id, result.Id);
    Assert.AreEqual(expectedDocumentEntity.Correspondent, result.Correspondent);
    Assert.AreEqual(expectedDocumentEntity.DocumentType, result.DocumentType);
    Assert.AreEqual(expectedDocumentEntity.Title, result.Title);
    Assert.AreEqual(expectedDocumentEntity.Content, result.Content);
    Assert.AreEqual(expectedDocumentEntity.Tags, result.Tags);
    Assert.AreEqual(expectedDocumentEntity.Created, result.Created);
    Assert.AreEqual(expectedDocumentEntity.Modified, result.Modified);
    Assert.AreEqual(expectedDocumentEntity.ArchiveSerialNumber, result.ArchiveSerialNumber);
    Assert.AreEqual(expectedDocumentEntity.OriginalFileName, result.OriginalFileName);
    Assert.AreEqual(expectedDocumentEntity.ArchivedFileName, result.ArchivedFileName);
    _repositoryMock.Verify(r => r.GetDocument(documentId), Times.Once);
    }
    [Test]
    public async Task CreateDocument_Should_Return_Id_When_Valid()
    {
    var documentEntity = new DocumentEntity
    {
        Title = "Valid Title",
        Id = 1,
        UploadDocument = new Mock<IFormFile>().Object 
    };
    _validatorMock.Setup(v => v.Validate(documentEntity)).Returns(new ValidationResult()); // Gültige Validierung
    _mapperMock.Setup(m => m.Map<DocumentDTO>(It.IsAny<DocumentEntity>())).Returns(new DocumentDTO());
    _repositoryMock.Setup(r => r.CreateDocument(It.IsAny<DocumentDTO>())).Returns(1); // Beispiel für eine erfolgreiche Erstellung

    var result = await _service.CreateDocument(documentEntity);

    Assert.AreEqual(1, result); 
    _repositoryMock.Verify(r => r.CreateDocument(It.IsAny<DocumentDTO>()), Times.Once);
    }

    [Test]
    public async Task CreateDocument_Should_Throw_Exception_When_Invalid()
    {
    var documentEntity = new DocumentEntity
    {
        Title = null, 
        Id = 1,
        UploadDocument = new Mock<IFormFile>().Object
    };
    _validatorMock.Setup(v => v.Validate(documentEntity)).Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Title", "Title is required") })); // Ungültige Validierung mit Fehlermeldung
    _mapperMock.Setup(m => m.Map<DocumentDTO>(It.IsAny<DocumentEntity>())).Returns(new DocumentDTO());
    _repositoryMock.Setup(r => r.CreateDocument(It.IsAny<DocumentDTO>())); 

    Assert.ThrowsAsync<Exception>(async () => await _service.CreateDocument(documentEntity));
    _repositoryMock.Verify(r => r.CreateDocument(It.IsAny<DocumentDTO>()), Times.Never); 
    }

    [Test]
    public async Task GetDocument_Should_Throw_Exception_When_Id_Is_Zero()
    {
    var documentId = 0; 
    _repositoryMock.Setup(r => r.GetDocument(documentId));

    Assert.ThrowsAsync<Exception>(async () => await _service.GetDocument(documentId));
    _repositoryMock.Verify(r => r.GetDocument(documentId),
        Times.Never); 
    }
}
