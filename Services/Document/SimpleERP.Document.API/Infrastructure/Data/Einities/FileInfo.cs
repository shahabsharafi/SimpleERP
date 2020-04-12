using SimpleERP.Libraries.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Document.API.Infrastructure.Data
{
    public class DocumentFile: IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public long DocumentInfoId { get; set; }
    }
}
