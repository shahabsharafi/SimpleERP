using SimpleERP.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Document.API.Infrastructure.Model
{
    public class DocumentInfoModel
    {
        public long Id { get; set; }
        public string  No { get; set; }
        public string Subject { get; set; }
        public string FilePath { get; set; }
        public string Text { get; set; }
        public string DateOfRelease { get; set; }
        public string DateOfCreate { get; set; }
        public string Creator { get; set; }
        public string DateOfModify { get; set; }
        public string Modifier { get; set; }
        public long IssuerId { get; set; }
        public string IssuerTitle { get; set; }
        public long DomainId { get; set; }
        public string DomainTitle { get; set; }
        public long TypeId { get; set; }
        public string TypeTitle { get; set; }
    }
}
