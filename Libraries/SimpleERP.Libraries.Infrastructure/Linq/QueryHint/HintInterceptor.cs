//Source : http://stackoverflow.com/a/26762756/790811

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;

namespace SimpleERP.Libraries.Infrastructure.Linq
{
    public class HintInterceptor : DbCommandInterceptor
    {
        internal static bool IsRegistered = false;
        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            if (interceptionContext.DbContext != null)
            {
                var ctx = interceptionContext.DbContext as IQueryHintContext;
                var addingString = $" option ({ctx.QueryHint})";
                if (ctx.ApplyHint && command.CommandText != null && !command.CommandText.Contains(addingString))
                {
                    command.CommandText += addingString;
                }
            }
            base.ReaderExecuting(command, interceptionContext);
        }

        internal static void Register()
        {
            DbInterception.Add(new HintInterceptor());
        }
    }
}
