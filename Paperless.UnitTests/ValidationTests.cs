using Paperless.Businesslogic.Entities;
using Paperless.Businesslogic.Logic.Validation;

namespace Paperless.UnitTests;

[TestFixture]
public class DocumentEntityValidationTests
{
    private DocumentEntityValidator validator;
    [SetUp]
    public void Setup()
    {
        validator = new DocumentEntityValidator();
    }

    [Test]
    public void Validator_Should_Have_Error_When_Title_Is_Null()
    {
        var model = new DocumentEntity { Title = null, Id = 1 }; // Stellen Sie sicher, dass Id gesetzt ist, um nur den Titel zu testen
        var result = validator.Validate(model);
        Assert.IsTrue(result.Errors.Any(e=>e.PropertyName == "Title"));
    }

    [Test]
    public void Validator_Should_Not_Have_Error_When_Id_And_Title_Are_Valid()
    {
        var model = new DocumentEntity { Title = "Valid Title", Id = 1 };
        var result = validator.Validate(model);
        Assert.IsFalse(result.Errors.Any(), "Validator sollte keine Fehler ausgeben, wenn Id und Titel gültig sind.");
    }

    [Test]
    public void Validator_Should_Have_Error_When_Title_Is_Empty()
    {
        var model = new DocumentEntity { Title = string.Empty, Id = 1 };
        var result = validator.Validate(model);
        Assert.IsTrue(result.Errors.Any(e => e.PropertyName == "Title" && e.ErrorMessage.Contains("Title required")), "Validator sollte einen Fehler ausgeben, wenn der Titel leer ist.");
    }

  
}