using AutoMapper;
using Paperless.Businesslogic.Entities;
using Paperless.DataAccessLayer.Entities;

namespace Paperless.Businesslogic.Logic.Mappers;
public class DocumentEntityToDtoMapper : Profile
{
    public DocumentEntityToDtoMapper()
    {
        CreateMap<DocumentDTO, DocumentEntity>().ReverseMap();
    }
}