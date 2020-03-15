using AutoMapper;
using SimpleERP.Document.API.Infrastructure.Data;

namespace SimpleERP.Document.API.Infrastructure.Models
{
    public class DocumentInfoProfile : Profile
    {
        public DocumentInfoProfile()
        {
            CreateMap<DocumentInfo, DocumentInfoModel>();
            CreateMap<DocumentInfoModel, DocumentInfo>();
        }
    }
}
