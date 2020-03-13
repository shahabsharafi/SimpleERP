using SimpleERP.Libraries.Infrastructure.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.QueryHandler
{
    public interface IQuerybleController
    {
        IQueryable GetQuery();
        Task<ActionResult> DeleteFromQueryAsync(IQueryable query, CancellationToken cancellationToken = default);
        Task<ActionResult> DeleteByIdAsync(string[] ids, CancellationToken cancellationToken = default);
        void CreateExcel(IQueryable query, IExcelHelper excelHelper);
    }
}
