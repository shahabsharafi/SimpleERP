using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SimpleERP.Libraries.API.Http;
using System.Collections.Specialized;
using SimpleERP.Libraries.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using SimpleERP.Libraries.Infrastructure.QueryHandler;
using IPMS.Services.Identity.API.Infrastructure.File;
using SimpleERP.Libraries.Infrastructure.Excel;
using SimpleERP.Libraries.API.Infrastructure;
using System.IO;

namespace SimpleERP.Libraries.API.Filters
{

    public class ExcelFilterAction : ActionFilterAttribute
    {
        private readonly IQueryHandler _queryHandler;
        IExcelHelper _excelHelper;
        IQueryable _query;
        public ExcelFilterAction(IQueryHandler queryHandler, IExcelHelper excelHelper) 
        {
            this._queryHandler = queryHandler;
            this._excelHelper = excelHelper;
            this._query = null;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (!context.ModelState.IsValid) return;

            if (context.HttpContext.Request.Query.Any(o => o.Key.ToUpper() == "ISEXCEL" && o.Value.ToString() == "1") &&
                context.Result is ObjectResult objectResult &&
                objectResult.Value is IEnumerable model &&
                context.Controller is IQuerybleController controller &&
                context.Controller is ControllerBase baseController)
            {
                IQueryable query = model.AsQueryable();
                var args = context.HttpContext.Request.Query;
                this._query = this._queryHandler.ApplyQuery(query, args);
                this._excelHelper.Init();
                controller.CreateExcel(this._query, this._excelHelper);
                this._query = null;
                var content = this._excelHelper.GetAsByteArray();
                context.Result = baseController.File(content, FileHelper.GetMime(OutputFileFormat.XLSX));
            }
        }
    }
}
