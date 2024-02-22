using AutoMapper;
using Paperless.Businesslogic.Entities;

namespace Paperless.REST.Mappers;

internal class DocumentToEntityMapper : Profile
{
    public DocumentToEntityMapper()
    {
        CreateMap<Document, DocumentEntity>().ReverseMap();
        CreateMap<UpdateDocumentRequest, DocumentEntity>().ReverseMap();
    }
}