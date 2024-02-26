using AutoMapper;
using Paperless.Businesslogic.Entities;
using Paperless.REST;
using Paperless.REST.Mappers;

namespace Paperless.UnitTests;

[TestFixture]
public class AutoMapperTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DocumentToEntityMapper());
        });
        _mapper = configuration.CreateMapper();
    }

    [Test]
    public void DocumentToDocumentEntity_Map_IsValid()
    {
        var document = new Document
        {
            Id = 1,
            Title = "Testdokument",
            Content = "Inhalt des Testdokuments",
        };

        var documentEntity = _mapper.Map<DocumentEntity>(document);

        Assert.IsNotNull(documentEntity);
    }

    [Test]
    public void UpdateDocumentRequestToDocumentEntity_Map_IsValid()
    {
    }

    [Test]
    public void DocumentEntity_Mapping_Properties_Are_Valid()
    {
        var document = new Document
        {
            Id = 1,
            Correspondent = 2,
            DocumentType = 3,
            Title = null,
            Content = "Test Content",
            Tags = new List<int> { 1, 2, 3 },
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Added = DateTime.Now,
            ArchiveSerialNumber = "12345",
            OriginalFileName = "original.pdf",
            ArchivedFileName = "archived.pdf",
        };
        var documentEntity = _mapper.Map<DocumentEntity>(document);

        Assert.That(documentEntity.Correspondent, Is.EqualTo(document.Correspondent));
        Assert.That(documentEntity.DocumentType, Is.EqualTo(document.DocumentType));
        Assert.That(documentEntity.Title, Is.EqualTo(document.Title));
        Assert.That(documentEntity.Content, Is.EqualTo(document.Content));
        Assert.That(documentEntity.Tags, Is.EqualTo(document.Tags));
        Assert.That(documentEntity.Created, Is.EqualTo(document.Created));
        Assert.That(documentEntity.Modified, Is.EqualTo(document.Modified));
        Assert.That(documentEntity.Added, Is.EqualTo(document.Added));
        Assert.That(documentEntity.ArchiveSerialNumber, Is.EqualTo(document.ArchiveSerialNumber));
        Assert.That(documentEntity.OriginalFileName, Is.EqualTo(document.OriginalFileName));
        Assert.That(documentEntity.ArchivedFileName, Is.EqualTo(document.ArchivedFileName));

    }
    
    [Test]
    public void DocumentEntityToDocument_Map_And_Back_IsValid()
    {
        var documentEntity = new DocumentEntity
        {
            Id = 1,
            Title = "Beispiel",
            Content = "Beispielinhalt"
        };

        // Zuerst von DocumentEntity zu Document mappen
        var document = _mapper.Map<Document>(documentEntity);

        // Dann zurück zu DocumentEntity mappen
        var mappedBackDocumentEntity = _mapper.Map<DocumentEntity>(document);

        // Überprüfen, ob die gemappten Objekte gleich dem Original sind
        Assert.AreEqual(documentEntity.Id, mappedBackDocumentEntity.Id);
        Assert.AreEqual(documentEntity.Title, mappedBackDocumentEntity.Title);
        Assert.AreEqual(documentEntity.Content, mappedBackDocumentEntity.Content);
    }


}