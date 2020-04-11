using SimpleERP.Libraries.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleERP.Document.API.Infrastructure.Data
{
    public class DocumentInfo: IEntity
    {
        public long Id { get; set; }
        public string  No { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string DateOfRelease { get; set; }
        public string DateOfCreate { get; set; }
        public string Creator { get; set; }
        public string DateOfModify { get; set; }
        public string Modifier { get; set; }
        public long IssuerId { get; set; }
        public Issuer Issuer { get; set; }
        public long DomainId { get; set; }
        public Domain Domain { get; set; }
        public long TypeId { get; set; }
        public Type Type { get; set; }
        public ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
