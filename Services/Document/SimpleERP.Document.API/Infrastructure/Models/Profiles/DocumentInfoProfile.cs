using AutoMapper;
using SimpleERP.Document.API.Infrastructure.Data;

namespace SimpleERP.Document.API.Infrastructure.Models
{
    public class DocumentInfoProfile : Profile
    {
        public DocumentInfoProfile()
        {
            CreateMap<DocumentInfo, DocumentInfoModel>().AfterMap((s, t) => {
                t.TypeTitle = s.Type.Title;
                t.IssuerTitle = s.Issuer.Title;
                t.DomainTitle = s.Domain.Title;
            }) ;
            CreateMap<DocumentInfoModel, DocumentInfo>();
        }
    }
}
