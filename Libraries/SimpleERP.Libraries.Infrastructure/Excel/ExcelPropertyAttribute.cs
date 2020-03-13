using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Libraries.Infrastructure.Excel
{
    /// <summary>
    /// An attribute class to keep properties (attribute) of a type.
    /// Properties of this type is used to as columns in excel file.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelPropertyAttribute : Attribute
    {
        public static readonly ExcelPropertyAttribute Default;
        /// <summary>
        /// Set a name for excel attribute.
        /// </summary>
        /// <param name="methodeName"></param>
        public ExcelPropertyAttribute([CallerMemberName] string methodeName = null)
        {
            this.MethodeName = methodeName;
        }
        /// <summary>
        /// this key use for get header text that is displayed in the first row of the worksheet.
        /// </summary>
        public string MethodeName { get; protected set; }
    }

    public class BooleanExcelPropertyAttribute: ExcelPropertyAttribute
    {
        public BooleanExcelPropertyAttribute([CallerMemberName] string methodeName = null, string trueKey = "true_key", string falseKey = "false_key") : base(methodeName)
        {
            this.TrueKey = trueKey;
            this.FalseKey = falseKey;
        }
        public string TrueKey { get; protected set; }
        public string FalseKey { get; protected set; }
    }
}
