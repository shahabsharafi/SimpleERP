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

namespace SimpleERP.Libraries.API.Filters
{

    public class QueryHandlerFilterAction : ActionFilterAttribute
    {
        private readonly IQueryHandler _queryHandler;
        public QueryHandlerFilterAction(IQueryHandler queryHandler) //: base(typeof(QueryHandlerFilterActionImpelementation))
        {
            this._queryHandler = queryHandler;
        }       

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) return;
            //if (context.HttpContext.Request.Method.ToUpper() == "DELETE" &&
            //    context.Controller is IQuerybleController controller)
            //{
            //    var args = context.HttpContext.Request.Query;
            //    if (this._queryHandler.HasFilter(args))
            //    {
            //        var query = controller.GetQuery();
            //        query = this._queryHandler.ApplyQuery(query, args);
            //        controller.DeleteFromQueryAsync(query);
            //    }
            //    else if (this._queryHandler.HasIds(args))
            //    {
            //        var ids = this._queryHandler.GetIds(args);
            //        controller.DeleteByIdAsync(ids);
            //    }
            //}
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) return;

            if (context.HttpContext.Request.Query.Any(o => o.Key.ToUpper() == "ISEXCEL" && o.Value.ToString() == "1")) return;

            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is IEnumerable model)
                {
                    IQueryable query = model.AsQueryable();
                    var args = context.HttpContext.Request.Query;
                    var result = this._queryHandler.Execute(query, args);
                    ((ObjectResult)context.Result).Value = result;
                }
            }
            base.OnActionExecuted(context);
        }
    }
}
