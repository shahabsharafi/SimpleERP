using AutoMapper;
using SimpleERP.Document.API.Infrastructure.Data;
using System.Linq;

namespace SimpleERP.Document.API.Infrastructure.Models
{
    public class DocumentInfoProfile : Profile
    {
        public DocumentInfoProfile()
        {
            CreateMap<DocumentInfo, DocumentInfoModel>().AfterMap((e, m) => {
                m.DocumetFileIds = e.DocumentFiles.Select(o => o.Id).ToArray();
            });
            CreateMap<DocumentInfoModel, DocumentInfo>();
        }
    }
}
