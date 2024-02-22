
using Paperless.Businesslogic.Entities;
using FluentValidation;

namespace Paperless.Businesslogic.Logic.Validation;

public class DocumentEntityValidator : AbstractValidator<DocumentEntity>
{
    public DocumentEntityValidator()
    {
        //Document Validated by ID and Title
            RuleFor(document => document.Title).NotNull().WithMessage("Title required");
            RuleFor(document => document.Title).NotEmpty().WithMessage("Title required");
            RuleFor(document => document.Id).NotNull().WithMessage("Id required");
            RuleFor(document => document.Id).NotEmpty().WithMessage("Id required");
    }
}