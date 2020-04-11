using SimpleERP.Libraries.Infrastructure.Entities;
using SimpleERP.Libraries.Infrastructure.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Document.API.Infrastructure.Models
{
    public class DocumentInfoModel
    {
        [ExcelProperty]
        public long? Id { get; set; }
        [ExcelProperty]
        public string  No { get; set; }
        [ExcelProperty]
        public string Subject { get; set; }
        public long[] DocumetFileIds { get; set; }
        [ExcelProperty]
        public string Text { get; set; }
        [ExcelProperty]
        public string DateOfRelease { get; set; }
        [ExcelProperty]
        public string DateOfCreate { get; set; }
        [ExcelProperty]
        public string Creator { get; set; }
        public string DateOfModify { get; set; }
        public string Modifier { get; set; }
        public long IssuerId { get; set; }
        [ExcelProperty]
        public string IssuerTitle { get; set; }
        public long DomainId { get; set; }
        [ExcelProperty]
        public string DomainTitle { get; set; }
        public long TypeId { get; set; }
        [ExcelProperty]
        public string TypeTitle { get; set; }
    }
}
